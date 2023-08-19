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

        public async Task<ImportedSchoolDto> ExtractSchoolDataFromXlsx(IFormFile file)
        {
            ///do poprawki, moze warto od razu tworzyc grupy?
            var school = new SchoolDto();
            var groups = new List<GroupDto>();

            using (var stream = file.OpenReadStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    var schoolWorksheet = package.Workbook.Worksheets[0];

                    school.Name = schoolWorksheet.Cells[1, 2].Text;
                    school.Address = new Address
                    {
                        Street = schoolWorksheet.Cells[2, 2].Text,
                        BuildingNumber = schoolWorksheet.Cells[3, 2].Text,
                        City = schoolWorksheet.Cells[4, 2].Text,
                        Country = schoolWorksheet.Cells[5, 2].Text
                    };

                    var groupWokrsheets = new List<GroupDto>();
                    var teachersSet = new HashSet<TeacherDto>();
                    var studentsSet = new HashSet<StudentDto>();

                    var row = 7;
                    while (!string.IsNullOrEmpty(schoolWorksheet.Cells[row, 2].Text))
                    {
                        var teacher = new TeacherDto()
                        {
                            FirstName = schoolWorksheet.Cells[row, 2].Text,
                            LastName = schoolWorksheet.Cells[row, 3].Text
                        };
                        groups.Add(new GroupDto { Name = schoolWorksheet.Cells[row, 2].Text });
                        row++;
                    }

                    if (groups.Count != package.Workbook.Worksheets.Count - 1)
                    {
                        throw new InvalidOperationException("Mismatch between group names and number of sheets.");
                    }

                    //school.Groups = groups;
                }
            }

            return new ImportedSchoolDto();// {SchoolDetails = school, ShortDataGroups = records }; ;
        }
    }
}
