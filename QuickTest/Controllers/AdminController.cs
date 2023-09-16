using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuickTest.Application.FileImporter;
using QuickTest.Application.FileImporter.BulkImport;
using QuickTest.Application.FileImporter.ImportSchoolData;
using QuickTest.Application.Schools.GetSchool;
using QuickTest.Application.Students.GetStudentsByGroup;
using QuickTest.Application.Teachers.GetTeachers;
using QuickTest.Application.Users.CreateUser;
using System.Data;

namespace QuickTest.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "administrator")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMemoryCache memoryCache;

        public AdminController(IMediator mediator, IMemoryCache memoryCache)
        {
            this.mediator = mediator;
            this.memoryCache = memoryCache;
        }

        [HttpGet("group/{groupId}/students")]
        public async Task<IActionResult> GetStudentsByGroup(int groupId)
        {
            var students = await this.mediator.Send(new GetStudentsByGroupRequest() { GroupId = groupId });

            return this.Ok(students);
        }

        [HttpGet("teachers")]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await this.mediator.Send(new GetTeachersRequest());

            return this.Ok(teachers);
        }

        [HttpGet("schools")]
        public async Task<IActionResult> GetSchools()
        {
            var schools = await this.mediator.Send(new GetSchoolRequest());

            return this.Ok(schools);
        }
        [HttpPost("import")]
        public async Task<IActionResult> ImportSchoolData([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var importRequest = new ImportSchoolDataRequest { File = file };
            var result = await this.mediator.Send(importRequest);

            if (!result.IsSuccess)
            {
                return BadRequest(string.Join(";",result.ErrorMessages));
            }
            var importId = Guid.NewGuid().ToString();
            memoryCache.Set(importId, result.ImportSummary, TimeSpan.FromMinutes(30)); 


             return Ok(new { ImportId = importId, Summary = result.ImportSummary });
        }
        [HttpGet("import/{importId}")]
        public IActionResult GetImportSummary(string importId)
        {
            if (memoryCache.TryGetValue(importId, out ImportSummaryDto importSummary))
            {
                Console.WriteLine("Import summary found.");
                return Ok(importSummary);
            }
            return NotFound("Import summary not found.");
        }
        [HttpDelete("clearCache/{importId}")]
        public IActionResult ClearCache(string importId)
        {
            if (memoryCache.TryGetValue(importId, out _))
            {
                memoryCache.Remove(importId);
                return Ok("Cache cleared.");
            }
            return NotFound("Import summary not found in cache.");
        }
        [HttpPost("bulk-import")]
        public async Task<IActionResult> BulkImport([FromBody] BulkImportRequest request)
        {
            if (request == null || request.ImportSummary == null)
            {
                return BadRequest("Invalid request or all groups are already created.");
            }

            var bulkImportRequest = new BulkImportHandlerRequest
            {
                ImportSummary = request.ImportSummary,
                SchoolId = request.SchoolId
            };

            var result = await this.mediator.Send(bulkImportRequest);

            if (!result.IsSuccess)
            {
                return BadRequest(new CreatedAccountsSummary
                {
                    IsSuccess = false,
                    ErrorList = result.ErrorList
                });
            }

            return Ok(new CreatedAccountsSummary
            {
                IsSuccess = true,
                ErrorList = result.ErrorList,
                TeacherCreationFailed = result.TeacherCreationFailed,
                TeachersCreated = result.TeachersCreated,
                GroupsCreated = result.GroupsCreated,
                GroupsFailed = result.GroupsFailed,
                StudentCreationFailed = result.StudentCreationFailed,
                StudentsCreated = result.StudentsCreated
                
            });
        }
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            var result = await this.mediator.Send(new CreateUserRequest { UserDto = userDto });

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok("User created successfully.");
        }
    }
}
