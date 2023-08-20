using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.FileImporter.ImportSchoolData;
using QuickTest.Application.Schools.GetSchool;
using QuickTest.Application.Students.GetStudentsByGroup;
using QuickTest.Application.Teachers.GetTeachers;
using QuickTest.Application.Users.CreateUser;
using System.Data;

namespace QuickTest.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator mediator;

        public AdminController(IMediator mediator)
        {
            this.mediator = mediator;
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
        public async Task<IActionResult> ImportSchoolData(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var importRequest = new ImportSchoolDataRequest { File = file };
            var result = await this.mediator.Send(importRequest);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.ImportSummary);
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
