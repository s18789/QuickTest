using Microsoft.AspNetCore.Http;
using QuickTest.Application.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter.FileReading
{
    public interface IFileReaderService
    {
        Task<List<GroupDto>> ExtractDataFromCsv(IFormFile file);
        Task<ImportedGroupsDto> ExtractSchoolDataFromXlsx(IFormFile file);
    }
}
