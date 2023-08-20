using CsvHelper;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using QuickTest.Application.Groups;
using QuickTest.Application.Schools;
using QuickTest.Application.Students;
using QuickTest.Application.Teachers;
using QuickTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter.FileReading
{
    public class FileReaderService : IFileReaderService
    {
        public async Task<List<GroupDto>> ExtractDataFromCsv(IFormFile file)
        {
            var records = new List<GroupDto>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<GroupDto>().ToList();
            }
            

            return records;
        }

        public async Task<ImportedGroupsDto> ExtractSchoolDataFromXlsx(IFormFile file)
        {
            var groupsSet = new HashSet<string>();
            var teachersSet = new HashSet<TeacherDto>();
            var studentsSet = new HashSet<StudentDto>();

            using (var stream = file.OpenReadStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    foreach (var schoolWorksheet in package.Workbook.Worksheets)
                    {
                        var row = 1;
                        while (!string.IsNullOrEmpty(schoolWorksheet.Cells[row, 1].Text))
                        {
                            
                            var groupName = schoolWorksheet.Cells[row, 1].Text;
                            groupsSet.Add(groupName);

                            var teacher = new TeacherDto()
                            {
                                FirstName = schoolWorksheet.Cells[row, 2].Text,
                                LastName = schoolWorksheet.Cells[row, 3].Text,
                                Email = schoolWorksheet.Cells[row, 4].Text
                            };
                            teachersSet.Add(teacher);

                            var student = new StudentDto()
                            {
                                FirstName = schoolWorksheet.Cells[row, 5].Text,
                                LastName = schoolWorksheet.Cells[row, 6].Text,
                                Email = schoolWorksheet.Cells[row, 7].Text
                            };
                            studentsSet.Add(student);

                            row++;
                        }
                    }
                }
            }

            return new ImportedGroupsDto
            {
                Groups = groupsSet.ToList(),
                Teachers = teachersSet.ToList(),
                Students = studentsSet.ToList()
            };
        }

    }
}
