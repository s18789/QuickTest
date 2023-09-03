using QuickTest.Application.Common.Enums;

namespace QuickTest.Application.ExamsResults.GetExamsResults;

public sealed record ExamResultDto
{
    public int Id { get; init; }

    public string ExamName { get; init; }

    public ExamResultStatus Status { get; init; }

    public double? Score { get; init; }

    public DateTime EndingDate { get; init; }
}
