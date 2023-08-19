using QuickTest.Application.Questions;

namespace QuickTest.Application.Exams.CreateExam;

public sealed record CreateExamDto
{
    public string Title { get; init; }

    public int Time { get; init; }

    public DateTime AvailableFrom { get; init; }

    public DateTime AvailableTo { get; init; }

    public IEnumerable<QuestionDTO> Questions { get; init; }

    public IEnumerable<Student> Students { get; init; }
}

public sealed record Student
{
    public int Id { get; init; }
}
