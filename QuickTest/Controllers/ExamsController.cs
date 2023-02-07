using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.Exams.CreateExam;
using QuickTest.Application.Exams.GetExam;
using QuickTest.Application.Exams.GetExams;
using QuickTest.Infrastructure.Data;

namespace QuickTest.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "Teacher")]
[ApiController]
public class ExamsController : ControllerBase
{
    private readonly IMediator mediator;

    public ExamsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetExams()
    {
        var exams = await this.mediator.Send(new GetExamsRequest());

        return this.Ok(exams);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExam(int id)
    {
        var exam = await this.mediator.Send(new GetExamRequest() { Id = id });

        return this.Ok(exam);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExamDto exam)
    {
        await this.mediator.Send(new CreateExamRequest() { Exam = exam });

        return Ok();
    }
}
