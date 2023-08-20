using QuickTest.Application.Common.Enums;

namespace QuickTest.Application.ExamsResults.GetExamResult;

public sealed record GetExamResultDto
{
    public ExamResultStatus Status { get; init; }

    public int? MaxPoints { get; init; }

    public double? Score { get; init; }

    public int? QuestionCount { get; init; }

    public int? CorrectAnswers { get; init; }

    public int? WrongAnswers { get; init; }

    public DateTime? StartTime { get; init; }

    public DateTime? EndTime { get; init; }
}
