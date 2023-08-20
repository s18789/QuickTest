using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.Exams.AddExamMembers;
using QuickTest.Application.Exams.CreateExam;
using QuickTest.Application.Exams.GetExam;
using QuickTest.Application.Exams.GetExamPreview;
using QuickTest.Application.Exams.GetExams;
using QuickTest.Infrastructure.Data;

namespace QuickTest.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "teacher")]
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

    [HttpGet("Preview/{id}")]
    public async Task<IActionResult> GetExamPreview(int id)
    {
        var exam = await this.mediator.Send(new GetExamPreviewRequest() { ExamId = id });

        return this.Ok(exam);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExamDto exam)
    {
        await this.mediator.Send(new CreateExamRequest() { Exam = exam });

        return Ok();
    }

    [HttpPost("addExamMembers")]
    public async Task<IActionResult> AddExamMembers([FromBody] ExamMembersDTO examMembers)
    {
        await this.mediator.Send(new AddExamMembersRequest() { ExamMembers = examMembers });

        return Ok();
    }
}
