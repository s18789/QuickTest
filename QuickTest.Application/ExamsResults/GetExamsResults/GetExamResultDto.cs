namespace QuickTest.Application.ExamsResults.GetExamsResults;

public sealed record GetExamResultDto
{
    public int Id { get; init; }

    public string ExamName { get; init; }

    public string Status { get; init; }

    public double? Score { get; init; }

    public DateTime EndingDate { get; init; }
}
