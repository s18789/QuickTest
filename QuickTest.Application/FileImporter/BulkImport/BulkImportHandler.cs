using AutoMapper;
using MediatR;
using QuickTest.Application.FileImporter.ImportSchoolData;
using QuickTest.Application.Groups.CreateGroup;
using QuickTest.Application.Students;
using QuickTest.Application.Teachers;
using QuickTest.Application.Users.CreateUser;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;
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



        public BulkImportHandler(CreateUserHandler createUserHandler, CreateGroupHandler createGroupHandler, IGroupRepository groupRepository, IMapper mapper, ISchoolRepository schoolRepository)
        {
            _createUserHandler = createUserHandler;
            _createGroupHandler = createGroupHandler;
            _groupRepository = groupRepository;
            _mapper = mapper;
            _schoolRepository = schoolRepository;
        }

        public async Task<CreatedAccountsSummary> Handle(BulkImportHandlerRequest bulkRequest, CancellationToken cancellationToken)
        {
            CreatedAccountsSummary createdAccountsSummary = new CreatedAccountsSummary();
            createdAccountsSummary.IsSuccess = false;
            foreach (var groupData in bulkRequest.ImportSummary.ImportedGroupsFromFile.ImportedGroups)
            {
                var teacher = groupData.Item2;
                var students = groupData.Item3;
                if (bulkRequest.ImportSummary.RecordsSummary.ExistingGroups.Contains(groupData.Item1))
                {
                    createdAccountsSummary.GroupsFailed++;
                    continue;
                }
                else
                {
                    var createGroupRequest = new CreateGroupRequest();
                    var school = await _schoolRepository.GetSchoolIncludeGroups(bulkRequest.SchoolId);
                    var successfulStudentsCreated = new List<StudentDto>();
                    var teacherToAdd = new TeacherDto();
                    var createdGroup = new Group();
                    if (school != null)
                    {
                        createGroupRequest.Group.School = school;
                        createdGroup = await _groupRepository.CreateGropuByName(groupData.Item1, school);
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
                                    UserRole = teacher.UserRole
                                }
                            };
                            ResponseDto teacherResponse = new ResponseDto();
                            try
                            {
                                teacherResponse = await _createUserHandler.Handle(teacherRequest, new CancellationToken());
                            }
                            catch (Exception ex)
                            {

                                createdAccountsSummary.ErrorList.Add("There was an issue with creating teacher "+teacherRequest.UserDto.LastName+ "+  account: "+ex.Message);
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
                                            UserRole = student.UserRole
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
                                        createdAccountsSummary.StudentsCreated++;
                                        successfulStudentsCreated.Add(studentResponse.AddedStudent);

                                    }
                                    else
                                    {
                                        createdAccountsSummary.StudentCreationFailed++;
                                    }
                                }
                            }
                            foreach (var student in successfulStudentsCreated)
                            {
                                createGroupRequest.Group.Students.Add(student);
                            }

                        }
                    }

                    createGroupRequest.Group.Students = successfulStudentsCreated;
                    createGroupRequest.Group.GroupTeachers.Add(new Groups.GroupTeacherDto
                    {
                        Group = createdGroup,
                        Teacher = _mapper.Map<Teacher>(teacherToAdd)
                    });
                    createGroupRequest.Group.Name = createdGroup.Name;
                    var groupResponse = await _createGroupHandler.Handle(createGroupRequest, new CancellationToken());

                    if (groupResponse != null)
                    {
                        createdAccountsSummary.GroupsCreated++;
                    }
                    else
                    {
                        createdAccountsSummary.GroupsFailed++;
                    }

                }


            }
            createdAccountsSummary.IsSuccess = true;
            return createdAccountsSummary;
        }
    }






}
