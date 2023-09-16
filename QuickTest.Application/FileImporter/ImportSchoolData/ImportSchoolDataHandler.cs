using AutoMapper;
using MediatR;
using QuickTest.Application.FileImporter.FileReading;
using QuickTest.Application.Groups.CreateGroup;
using QuickTest.Application.Students;
using QuickTest.Application.Students.CreateStudent;
using QuickTest.Application.Teachers;
using QuickTest.Application.Teachers.CreateTeacher;
using QuickTest.Application.Users.CreateUser;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter.ImportSchoolData
{
    public class ImportSchoolDataHandler : IRequestHandler<ImportSchoolDataRequest, ImportSchoolDataResponse>
    {
        private readonly IUserRoleRepository roleRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IGroupRepository groupRepository;
        public ImportSchoolDataHandler(IUserRoleRepository userRoleRepository, ITeacherRepository teacherRepository, IStudentRepository studentRepository, IGroupRepository groupRepository) 
        {
            this.roleRepository = userRoleRepository;
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<ImportSchoolDataResponse> Handle(ImportSchoolDataRequest request, CancellationToken cancellationToken)
        {
            var fileExtension = Path.GetExtension(request.File.FileName).ToLower();
            var errorsList = new List<string>();

            if (fileExtension != ".xlsx")
            {
                errorsList.Add("Invalid file format. Please upload .xlsx file.");
                return new ImportSchoolDataResponse
                {
                    IsSuccess = false,
                    ErrorMessages =errorsList 
                };
            }
            var readerService = new FileReaderService(roleRepository);
            var extractedData = await readerService.ExtractSchoolDataFromXlsx(request.File);
            var existingRecordsSummary = await CheckExistingRecords(extractedData);
            var importSummary = new ImportSummaryDto
            {
                ExistingTeachers = existingRecordsSummary.ExistingTeacherEmails.Count,
                ExistingStudents = existingRecordsSummary.ExistingStudentEmails.Count,
                RecordsSummary = existingRecordsSummary,
                ImportedGroupsFromFile = extractedData
            };

            return new ImportSchoolDataResponse
            {
                IsSuccess = true,
                ImportSummary = importSummary
            };
        }
        
        public async Task<ExistingRecordsSummary> CheckExistingRecords(ImportedGroupsDto extractedData)
        {
            var summary = new ExistingRecordsSummary();

            foreach (var groupData in extractedData.ImportedGroups)
            {
                var group = await groupRepository.CheckIfGroupExists(groupData.Item1);
                if (group)
                {
                    summary.ExistingGroups.Add(groupData.Item1);
                }
                var teacher = groupData.Item2;
                if (await teacherRepository.CheckIfTeacherExists(teacher.Email))
                {
                    summary.ExistingTeacherEmails.Add(teacher.Email);
                }

                var students = groupData.Item3;
                foreach (var student in students)
                {
                    if (await studentRepository.CheckIfStudentExists(student.Email))
                    {
                        summary.ExistingStudentEmails.Add(student.Email);
                    }
                }
            }

            return summary;
        }
        
    }
    

}
