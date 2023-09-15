using QuickTest.Application.Common.Enums;

namespace QuickTest.Application.Exams.GetExam;

public sealed record ExamDto
{
    public int Id { get; init; }

    public string Title { get; init; }

    public ExamStatus Status { get; init; }

    public int QuestionsCount { get; init; }

    public DateTime AvailableFrom { get; init; }

    public DateTime AvailableTo { get; init; }

    public double? Average { get; init; }

    public QuestionDto HardQuestion { get; set; }

    public IEnumerable<ExamResultDto> ExamResults { get; init; }
}

public sealed record QuestionDto
{
    public int Index { get; init; }

    public double Average { get; init; }
}

    public sealed record ExamResultDto
{
    public int? Id { get; init; }

    public string FullName { get; init; }

    public string Email { get; init; }

    public ExamResultStatus Status { get; init; }

    public DateTime? FinishTime { get; init; }

    public double? Score { get; init; }
}
