using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.Groups.GetGroup;
using QuickTest.Application.Students;
using QuickTest.Application.Students.AddStudentToGroup;
using QuickTest.Application.Students.CreateStudent;
using QuickTest.Application.Students.GetStudent;
using QuickTest.Application.Students.GetStudents;
using QuickTest.Application.Students.GetStudentsForExam;
using QuickTest.Application.Students.RemoveStudent;

using QuickTest.Application.Students.UpdateStudent;

namespace QuickTest.Controllers;
[Route("api/[controller]")]
[Authorize(Roles = "teacher")]
[ApiController]

public class StudentsController : ControllerBase
{
    private readonly IMediator mediator;

    public StudentsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        var students = await this.mediator.Send(new GetStudentsRequest());

        return this.Ok(students);
    }

    [HttpGet("ForExam/{id}")]
    public async Task<IActionResult> GetStudentsForExam(int id)
    {
        var student = await this.mediator.Send(new GetStudentsForExamRequest() { Id = id });

        return this.Ok(student);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudent(int id)
    {
        var student = await this.mediator.Send(new GetStudentRequest() { Id = id });

        return this.Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] StudentDto student)
    {
        await this.mediator.Send(new CreateStudentRequest() { Student = student });

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudent([FromBody] StudentDto student)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await this.mediator.Send(new UpdateStudentRequest() { Student = student });

        if (result == null)
        {
            return NotFound($"Student with ID {student.Id} not found.");
        }

        return Ok(result);
    }
    [HttpDelete("{studentId}")]
    public async Task<IActionResult> RemoveStudent(int studentId)
    {
        var result = await this.mediator.Send(new RemoveStudentRequest() { StudentId = studentId });

        if (!result)
        {
            return NotFound($"Student with ID {studentId} not found.");
        }

        return Ok();
    }

    [HttpPost("{studentId}/groups")]
    public async Task<IActionResult> AddStudentToGroups(int studentId, [FromBody] List<int> groupIds)
    {
        var student = await this.mediator.Send(new GetStudentRequest() { Id = studentId });
        if (student == null)
        {
            return NotFound($"Student with ID {studentId} not found.");
        }

        foreach (var groupId in groupIds)
        {
            var group = await this.mediator.Send(new GetGroupRequest() { Id = groupId });
            if (group == null)
            {
                return NotFound($"Group with ID {groupId} not found.");
            }
        }

        var result = await this.mediator.Send(new AddStudentToGroupsRequest()
        {
            StudentId = studentId,
            GroupIds = groupIds
        });

        if (!result)
        {
            return BadRequest($"Failed to add student with ID {studentId} to the specified groups.");
        }

        return Ok();
    }
}
