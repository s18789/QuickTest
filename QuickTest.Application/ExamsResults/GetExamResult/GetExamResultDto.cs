namespace QuickTest.Application.ExamsResults.GetExamResult;

public class GetExamResultDto
{
    public string? Status { get; set; }

    public int? MaxPoints { get; set; }

    public double? Score { get; set; }

    public int? QuestionCount { get; set; }

    public int? CorrectAnswers { get; set; }

    public int? WrongAnswers { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }
}
