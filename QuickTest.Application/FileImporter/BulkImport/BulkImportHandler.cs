using AutoMapper;
using MediatR;
using QuickTest.Application.ExamsResults.GetExamsResultsToCheck;
using QuickTest.Application.FileImporter.ImportSchoolData;
using QuickTest.Application.Groups;
using QuickTest.Application.Groups.CreateGroup;
using QuickTest.Application.Schools;
using QuickTest.Application.Students;
using QuickTest.Application.Teachers;
using QuickTest.Application.Users.CreateUser;
using QuickTest.Application.Users.UserRole;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;
using QuickTest.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter.BulkImport
{
    public class BulkImportHandler : IRequestHandler<BulkImportHandlerRequest, CreatedAccountsSummary>
    {
        private readonly CreateUserHandler _createUserHandler;
        private readonly CreateGroupHandler _createGroupHandler;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IUserRepository _userRepository;
        private CreatedAccountsSummary createdAccountsSummary;
        private readonly IUserRoleRepository _userRoleRepository;



        public BulkImportHandler(CreateUserHandler createUserHandler, CreateGroupHandler createGroupHandler, IGroupRepository groupRepository,
            IMapper mapper, ISchoolRepository schoolRepository, IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            _createUserHandler = createUserHandler;
            _createGroupHandler = createGroupHandler;
            _groupRepository = groupRepository;
            _mapper = mapper;
            _schoolRepository = schoolRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;

        }

        public async Task<CreatedAccountsSummary> Handle(BulkImportHandlerRequest bulkRequest, CancellationToken cancellationToken)
        {
            createdAccountsSummary = new CreatedAccountsSummary();
            createdAccountsSummary.IsSuccess = false;
            createdAccountsSummary.ErrorList = new List<string>();
            HashSet<string> existingGroups = new HashSet<string>();
            HashSet<string> distinctStudents = new HashSet<string>();
            var alreadyAddedGroups = new HashSet<string>();
            foreach (var groupData in bulkRequest.ImportSummary.ImportedGroupsFromFile.ImportedGroups)
            {
                var teacher = _mapper.Map<TeacherDto>(groupData.Item2);
                var students = groupData.Item3;
                if (bulkRequest.ImportSummary.RecordsSummary.ExistingGroups.Contains(groupData.Item1))
                {
                    if (existingGroups.Contains(groupData.Item1))
                    {
                        continue;
                    }
                    else
                    {
                        createdAccountsSummary.GroupsFailed++;
                        createdAccountsSummary.ErrorList.Add("Group " + groupData.Item1 + " already exists, no new students or teachers will be added");
                        existingGroups.Add(groupData.Item1);
                        continue;
                    }
                    
                }
                else
                {
                    var createGroupRequest = new CreateGroupRequest();
                    var school = await _schoolRepository.GetSchoolWithoutGroups(bulkRequest.SchoolId);
                    var studentsToCreateGroup = new HashSet<StudentDto>();
                    var teacherToAdd = new TeacherDto();
                    var createdGroup = new GroupDto();
                    var teacherRole = await _userRoleRepository.GetRoleByType(Core.Entities.Enums.RoleType.TEACHER);
                    if (school != null)
                    {
                        createGroupRequest.Group = new Groups.GroupDto { School = _mapper.Map<SchoolDto>(school)};
                        var groupFromDb = await _groupRepository.CreateGroupByName(groupData.Item1, school);
                        //groupFromDb = await _groupRepository.GetByIdAsync(8);
                        createdGroup = _mapper.Map<GroupDto>(groupFromDb);
                        if(createdGroup.Id == 0)
                        {
                            createdAccountsSummary.ErrorList.Add("There was an issue with adding the group:  " + groupFromDb.Name + " .Group wont be created");
                            continue;
                        }
                        else
                        {
                            if (!bulkRequest.ImportSummary.RecordsSummary.ExistingTeacherEmails.Contains(teacher.Email))
                            {
                                var teacherRequest = new CreateUserRequest
                                {
                                    UserDto = new CreateUserDto
                                    {
                                        Email = teacher.Email,
                                        FirstName = teacher.FirstName,
                                        LastName = teacher.LastName,
                                        UserName = teacher.UserName,
                                        UserRole = _mapper.Map<UserRoleDto>(teacherRole)

                                    }
                                };
                                ResponseDto teacherResponse = new ResponseDto();
                                try
                                {
                                    teacherResponse = await _createUserHandler.Handle(teacherRequest, new CancellationToken());
                                }
                                catch (Exception ex)
                                {

                                    createdAccountsSummary.ErrorList.Add("There was an issue with creating teacher " + teacherRequest.UserDto.LastName + "+  account: " + ex.Message);
                                }
                                if (teacherResponse.IsSuccess)
                                {
                                    createdAccountsSummary.TeachersCreated++;
                                    teacherToAdd = teacherResponse.AddedTeacher;
                                }
                                else
                                {
                                    createdAccountsSummary.TeacherCreationFailed++;
                                    continue;
                                }


                            }
                            else
                            {
                                var existingTeacher = await _userRepository.GetTeacherByEmailAsync(teacher.Email);
                                if (existingTeacher != null)
                                {
                                    var teacherDto = _mapper.Map<TeacherDto>(existingTeacher);
                                    teacherToAdd = teacherDto;
                                }
                                else
                                {
                                    createdAccountsSummary.ErrorList.Add("Could not find existing teacher with email: " + teacher.Email);
                                    continue;
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        createdAccountsSummary.ErrorList.Add("Could not find school asociated with the admin. Import aborted");
                        createdAccountsSummary.IsSuccess = false;
                        return createdAccountsSummary;
                    }
                    studentsToCreateGroup = await CreateOrUpdateStudents(groupData.Item3, createdAccountsSummary, bulkRequest, createdGroup);
                    createdGroup.Students = new List<StudentDto>();
                    foreach (var student in studentsToCreateGroup)
                    {
                        if (student == null || string.IsNullOrEmpty(student.Email))
                            continue;
                        createdGroup.Students.Add(student);
                        distinctStudents.Add(student.Email);

                    }

                    createdGroup.GroupTeachers = new List<GroupTeacherDto>
                    {
                        new Groups.GroupTeacherDto
                        {
                            Group = createdGroup,
                            Teacher =teacherToAdd
                        }
                    };
                    createGroupRequest.Group=createdGroup;
                    //_schoolRepository.DetachThatMfcker(createGroupRequest.Group.School);
                    var groupResponse = await _createGroupHandler.HandleGroupTeacher(createGroupRequest, new CancellationToken());
                    

                    if (groupResponse != null)
                    {
                        if (!alreadyAddedGroups.Contains(groupResponse.Name))
                        {
                            createdAccountsSummary.GroupsCreated++;
                            alreadyAddedGroups.Add(groupResponse.Name);
                        }
                    }
                    else
                    {
                        createdAccountsSummary.GroupsFailed++;
                        createdAccountsSummary.ErrorList.Add("Could not create group with listed students and teachers. Group name: "+ groupData.Item1);
                    }

                }


            }
            createdAccountsSummary.StudentsCreated = distinctStudents.Count();
            createdAccountsSummary.IsSuccess = true;
            return createdAccountsSummary;
        }




        private async Task<HashSet<StudentDto>> CreateOrUpdateStudents(HashSet<QuickTest.Core.Entities.Student> students, CreatedAccountsSummary createdAccountsSummary, BulkImportHandlerRequest bulkRequest, GroupDto createdGroup)
        {
            var studentsToCreateGroup = new HashSet<StudentDto>();
            var studentRole = await _userRoleRepository.GetRoleByType(Core.Entities.Enums.RoleType.STUDENT);
            foreach (var student in students)
            {
                if (!bulkRequest.ImportSummary.RecordsSummary.ExistingStudentEmails.Contains(student.Email))
                {
                    var studentRequest = new CreateUserRequest
                    {
                        UserDto = new CreateUserDto
                        {
                            Email = student.Email,
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            UserName = student.UserName,
                            UserRole = _mapper.Map<UserRoleDto>(studentRole),
                            Group = createdGroup,
                            
                        }
                    };
                    ResponseDto studentResponse = new ResponseDto();
                    try
                    {
                        studentResponse = await _createUserHandler.Handle(studentRequest, new CancellationToken());
                        
                    }
                    catch (Exception ex)
                    {
                        createdAccountsSummary.ErrorList.Add("There was an issue with creating student " + studentRequest.UserDto.LastName + "+  account: " + ex.Message);
                    }
                    if (studentResponse.IsSuccess)
                    {
                        studentsToCreateGroup.Add(studentResponse.AddedStudent);
                    }
                    else
                    {
                        createdAccountsSummary.StudentCreationFailed++;
                    }
                }
                else
                {
                    studentsToCreateGroup.Add(_mapper.Map<StudentDto>(student));
                }
            }
            return studentsToCreateGroup;
        }
    }






}
