using CsvHelper;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using QuickTest.Application.Groups;
using QuickTest.Application.Schools;
using QuickTest.Application.Students;
using QuickTest.Application.Teachers;
using QuickTest.Core.Entities;
using QuickTest.Core.Entities.Enums;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter.FileReading
{
    public class FileReaderService : IFileReaderService
    {
        private readonly IUserRoleRepository roleRepository;
        public FileReaderService(IUserRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<ImportedGroupsDto> ExtractSchoolDataFromXlsx(IFormFile file)
        {
            var groupsWithTeacherAndStudents = new List<Tuple<string, Teacher, HashSet<Student>>>();
            var errorList = new List<string>();
            UserRole teacherRole = new UserRole();
            UserRole studentRole = new UserRole();
            try
            {
                teacherRole = await this.roleRepository.GetRoleByType(RoleType.TEACHER);
                studentRole = await this.roleRepository.GetRoleByType(RoleType.STUDENT);
            }
            catch (Exception ex)
            {

                errorList.Add("We were unable to get teacher and student role. Exception: "+ex.Message);
            }
            
            var failedTeacherReadCount = 0;
            var successTeacherReadCount = 0;
            var failedStudentReadCount = 0;
            var successStudentReadCount = 0;
            var failedGroupReadCount = 0;
            var successGroupReadCount = 0;

            string emailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        foreach (var schoolWorksheet in package.Workbook.Worksheets)
                        {
                            var groupName = schoolWorksheet.Name;

                            if (string.IsNullOrEmpty(groupName))
                            {
                                failedGroupReadCount++;
                                continue;
                            }
                            else
                            {
                                successGroupReadCount++;
                            }

                            var group = groupName;
                            var studentsSet = new HashSet<Student>();

                            var row = 1;
                            while (row <= schoolWorksheet.Dimension.End.Row)
                            {
                                var teacher = new Teacher()
                                {
                                    FirstName = schoolWorksheet.Cells[row, 1].Text,
                                    LastName = schoolWorksheet.Cells[row, 2].Text,
                                    Email = schoolWorksheet.Cells[row, 3].Text,
                                    UserName = schoolWorksheet.Cells[row, 4].Text.Split('@')[0],
                                    UserRole = teacherRole
                                };

                                if (string.IsNullOrEmpty(teacher.FirstName) || string.IsNullOrEmpty(teacher.LastName) || string.IsNullOrEmpty(teacher.Email) || string.IsNullOrEmpty(teacher.UserName))
                                {
                                    row++;
                                    failedTeacherReadCount++;
                                    continue;
                                }

                                if (!Regex.IsMatch(teacher.Email, emailPattern))
                                {
                                    row++;
                                    failedTeacherReadCount++;
                                    continue;
                                }
                                successTeacherReadCount++;

                                var studentRow = 1;
                                while (studentRow <= schoolWorksheet.Dimension.End.Row)
                                {
                                    var student = new Student()
                                    {
                                        FirstName = schoolWorksheet.Cells[studentRow, 5].Text,
                                        LastName = schoolWorksheet.Cells[studentRow, 6].Text,
                                        Email = schoolWorksheet.Cells[studentRow, 7].Text,
                                        UserName = schoolWorksheet.Cells[studentRow, 7].Text.Split('@')[0],
                                        UserRole = studentRole
                                    };

                                    if (string.IsNullOrEmpty(student.FirstName) || string.IsNullOrEmpty(student.LastName) || string.IsNullOrEmpty(student.Email) || string.IsNullOrEmpty(student.UserName))
                                    {
                                        failedStudentReadCount++;
                                        studentRow++;
                                        continue;
                                    }

                                    if (!Regex.IsMatch(student.Email, emailPattern))
                                    {
                                        failedStudentReadCount++;
                                        studentRow++;
                                        continue;
                                    }

                                    studentsSet.Add(student);
                                    successStudentReadCount++;
                                    studentRow++;
                                }

                                groupsWithTeacherAndStudents.Add(new Tuple<string, Teacher, HashSet<Student>>(group, teacher, studentsSet));
                                row++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                errorList.Add("We were unable to read the file. Exception: "+ ex.ToString());
            }
            

            return new ImportedGroupsDto
            {
                ImportedGroups = groupsWithTeacherAndStudents,
                SuccessfulStudentLoads = successStudentReadCount,
                SuccessfulTeacherLoads = successTeacherReadCount,
                FailedStudentLoads = failedStudentReadCount,
                FailedTeacherLoads =  failedTeacherReadCount,
                SuccessfulGroupLoads = successGroupReadCount,
                FailedGroupLoads = failedGroupReadCount,
                ErrorList = errorList
                
            };
        }


    }
}
