using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.Exams.GetExamPreview;
using QuickTest.Application.ExamsResults.FinishExam;
using QuickTest.Application.ExamsResults.GetExamResult;
using QuickTest.Application.ExamsResults.GetExamResultPreview;
using QuickTest.Application.ExamsResults.GetExamResultStatus;
using QuickTest.Application.ExamsResults.GetExamsResults;
using QuickTest.Application.ExamsResults.StartExam;

namespace QuickTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamsResultsController : ControllerBase
{
    private readonly IMediator mediator;

    public ExamsResultsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetExamsResults([FromQuery] int studentId)
    {
        var examsResults = await this.mediator.Send(new GetExamsResultsRequest() { StudentId = studentId } );

        return this.Ok(examsResults);
    }

    [HttpGet("StartExam")]
    public async Task<IActionResult> StartExam([FromQuery] int examResultId)
    {
        var examsResults = await this.mediator.Send(new StartExamRequest() { ExamResultId = examResultId });

        return this.Ok(examsResults);
    }

    [HttpGet("GetStatus")]
    public async Task<IActionResult> GetStatus([FromQuery] int examResultId)
    {
        var status = await this.mediator.Send(new GetExamResultStatusRequest() { ExamResultId = examResultId });

        return this.Ok(status);
    }


    [HttpGet("{examResultId}")]
    public async Task<IActionResult> GetExamResult(int examResultId)
    {
        var exam = await this.mediator.Send(new GetExamResultRequest() { ExamResultId = examResultId });

        return this.Ok(exam);
    }


    [HttpPost("FinishExam")]
    public async Task<IActionResult> FinishExam([FromBody] FinishExamDto exam)
    {
        await this.mediator.Send(new FinishExamRequest() { Exam = exam });

        return Ok();
    }

    [HttpGet("Preview/{id}")]
    public async Task<IActionResult> GetExamResultPreview(int id)
    {
        var exam = await this.mediator.Send(new GetExamResultPreviewRequest() { ExamResultId = id });

        return this.Ok(exam);
    }
}
