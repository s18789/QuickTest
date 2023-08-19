namespace QuickTest.Application.Exams.GetExam;

public sealed record ExamDto
{
    public int Id { get; init; }

    public string Name { get; init; }

    public string Status { get; init; }

    public string Category { get; init; }

    public int QuestionNumber { get; init; }

    public DateTime AvailableFrom { get; init; }

    public DateTime AvailableTo { get; init; }

    public int Time { get; init; }

    public IEnumerable<ExamResultDto> ExamResults { get; init; }
}

public sealed record ExamResultDto
{
    public int? Id { get; init; }

    public string FullName { get; init; }

    public string Email { get; init; }

    public string Status { get; init; }

    public DateTime? FinishTime { get; init; }

    public double? Score { get; init; }
}
