using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.Students;
using QuickTest.Application.Students.CreateStudent;
using QuickTest.Application.Students.GetStudent;
using QuickTest.Application.Students.GetStudents;
using QuickTest.Application.Students.UpdateStudent;

namespace QuickTest.Controllers;
[Route("api/[controller]")]
[Authorize(Roles = "Teacher")]
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
        await this.mediator.Send(new UpdateStudentRequest() { Student = student });

        return Ok();
    }
}
