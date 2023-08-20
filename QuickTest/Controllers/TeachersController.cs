using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.Groups.GetGroup;
using QuickTest.Application.Teachers;
using QuickTest.Application.Teachers.AddTeacherToGroups;
using QuickTest.Application.Teachers.CreateTeacher;
using QuickTest.Application.Teachers.GetTeacher;
using QuickTest.Application.Teachers.GetTeachers;
using QuickTest.Application.Teachers.RemoveTeacher;
using QuickTest.Application.Teachers.UpdateTeacher;
using System.Data;

namespace QuickTest.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "administrator")] 
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
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherDto teacherDto)
        {
            if (teacherDto == null)
            {
                return BadRequest("Teacher data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await this.mediator.Send(new CreateTeacherRequest() { Teacher = teacherDto });

            if (result == null)
            {
                return StatusCode(500, "A problem occurred while handling your request.");
            }

            return CreatedAtRoute("GetTeacher", new { id = result.Id }, result);
        }
        [HttpDelete("{teacherId}")]
        public async Task<IActionResult> RemoveTeacher(int teacherId)
        {
            var result = await this.mediator.Send(new RemoveTeacherRequest() { TeacherId = teacherId });

            if (!result)
            {
                return NotFound($"Teacher with ID {teacherId} not found.");
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacher([FromBody] TeacherDto teacher)
        {
            if (teacher == null || !teacher.Id.HasValue)
            {
                return BadRequest("Invalid teacher data.");
            }

            var updatedTeacher = await this.mediator.Send(new UpdateTeacherRequest() { Teacher = teacher });

            if (updatedTeacher == null)
            {
                return NotFound($"Teacher with ID {teacher.Id} not found.");
            }

            return Ok(updatedTeacher);
        }
        [HttpPost("{teacherId}/groups")]
        public async Task<IActionResult> AddTeacherToGroups(int teacherId, [FromBody] List<int> groupIds)
        {
            var teacher = await this.mediator.Send(new GetTeacherRequest() { Id = teacherId });
            if (teacher == null)
            {
                return NotFound($"Teacher with ID {teacherId} not found.");
            }

            foreach (var groupId in groupIds)
            {
                var group = await this.mediator.Send(new GetGroupRequest() { Id = groupId });
                if (group == null)
                {
                    return NotFound($"Group with ID {groupId} not found.");
                }
            }

            var result = await this.mediator.Send(new AddTeacherToGroupsRequest()
            {
                TeacherId = teacherId,
                GroupIds = groupIds
            });

            if (!result)
            {
                return BadRequest($"Failed to add teacher with ID {teacherId} to the specified groups.");
            }

            return Ok();
        }
    }
}
