using QuickTest.Application.Common.Enums;

namespace QuickTest.Application.ExamsResults.GetExamsResults;

public sealed record ExamResultDto
{
    public LastCompletedDto? LastCompleted { get; init; }

    public double? StudentAverage { get; init; }

    public double? Best { get; init; }

    public double? Average { get; init; }

    public double? Worst { get; init; }

    public IEnumerable<ExamResultGridItemDto> ExamsResultsGridItems { get; init; }
}

public sealed record LastCompletedDto
{
    public string Title { get; init; }

    public DateTime CompletionDate { get; init; }

    public double? Score { get; init; }

    public double? ComparisonToOthers { get; init; }
}

public sealed record ExamResultGridItemDto
{
    public int Id { get; init; }

    public string ExamName { get; init; }

    public ExamResultStatus Status { get; init; }

    public double? Score { get; init; }

    public DateTime EndingDate { get; init; }
}
