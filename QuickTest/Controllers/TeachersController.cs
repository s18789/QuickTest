using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.Teachers;
using QuickTest.Application.Teachers.CreateTeacher;
using QuickTest.Application.Teachers.GetTeacher;
using QuickTest.Application.Teachers.GetTeachers;
using QuickTest.Application.Teachers.UpdateTeacher;
using System.Data;

namespace QuickTest.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] 
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IMediator mediator;

        public TeachersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await this.mediator.Send(new GetTeachersRequest());

            return this.Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await this.mediator.Send(new GetTeacherRequest() { Id = id });

            return this.Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherDto teacher)
        {
            await this.mediator.Send(new CreateTeacherRequest() { Teacher = teacher });

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacher([FromBody] TeacherDto teacher)
        {
            await this.mediator.Send(new UpdateTeacherRequest() { Teacher = teacher });

            return Ok();
        }
    }
}
