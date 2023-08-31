using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.ExamsResults.CheckExam;
using QuickTest.Application.ExamsResults.FinishExam;
using QuickTest.Application.ExamsResults.GetCalendarExamsResults;
using QuickTest.Application.ExamsResults.GetCompletedExamsResults;
using QuickTest.Application.ExamsResults.GetExamResult;
using QuickTest.Application.ExamsResults.GetExamResultPreview;
using QuickTest.Application.ExamsResults.GetExamsResults;
using QuickTest.Application.ExamsResults.GetExamsResultsHeader;
using QuickTest.Application.ExamsResults.GetExamsResultsToCheck;
using QuickTest.Application.ExamsResults.GetExamsResultsToResolve;
using QuickTest.Application.ExamsResults.GetScheduleExams;
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
        var examsResults = await this.mediator.Send(new GetExamsResultsRequest() { StudentId = studentId });

        return this.Ok(examsResults);
    }

    [HttpGet("StartExam")]
    public async Task<IActionResult> StartExam([FromQuery] int examResultId)
    {
        var examsResults = await this.mediator.Send(new StartExamRequest() { ExamResultId = examResultId });

        return this.Ok(examsResults);
    }


    [HttpGet("{examResultId}")]
    public async Task<IActionResult> GetExamResult(int examResultId)
    {
        var exam = await this.mediator.Send(new GetExamResultRequest() { ExamResultId = examResultId });

        return this.Ok(exam);
    }

    [HttpGet("Preview/{id}")]
    public async Task<IActionResult> GetExamResultPreview(int id)
    {
        var exam = await this.mediator.Send(new GetExamResultPreviewRequest() { ExamResultId = id });

        return this.Ok(exam);
    }

    [HttpGet("ExamsResultsToResolve")]
    public async Task<IActionResult> GetExamsResultsToResolve()
    {
        var exams = await this.mediator.Send(new GetExamsResultsToResolveRequest());

        return this.Ok(exams);
    }

    [HttpGet("CompletedExamsResults")]
    public async Task<IActionResult> GetCompletedExamsResults()
    {
        var exams = await this.mediator.Send(new GetCompletedExamsResultsRequest());

        return this.Ok(exams);
    }

    [HttpGet("ExamsResultsToCheck")]
    public async Task<IActionResult> GetExamsResultsToCheck()
    {
        var exams = await this.mediator.Send(new GetExamsResultsToCheckRequest());

        return this.Ok(exams);
    }

    [HttpGet("CalendarExamsResults/{month}/{year}")]
    public async Task<IActionResult> GetCalendarExamsResults(int month, int year)
    {
        var exam = await this.mediator.Send(new GetCalendarExamsResultsRequest() { Month = month, Year = year });

        return this.Ok(exam);
    }

    [HttpGet("ExamsResultsHeader/{month}/{year}")]
    public async Task<IActionResult> GetExamsResultsHeader(int month, int year)
    {
        var exam = await this.mediator.Send(new GetExamsResultsHeaderRequest() { Month = month, Year = year });

        return this.Ok(exam);
    }

    [HttpGet("ScheduleExams")]
    public async Task<IActionResult> GetScheduleExams()
    {
        var exams = await this.mediator.Send(new GetScheduleExamsRequest());

        return this.Ok(exams);
    }

    [HttpPost("FinishExam")]
    public async Task<IActionResult> FinishExam([FromBody] FinishExamDto exam)
    {
        await this.mediator.Send(new FinishExamRequest() { Exam = exam });

        return Ok();
    }

    [HttpPost("CheckExam")]
    public async Task<IActionResult> CheckExam([FromBody] CheckedExamDTO checkedExam)
    {
        await this.mediator.Send(new CheckExamRequest() { CheckedExam = checkedExam });

        return Ok();
    }
}
