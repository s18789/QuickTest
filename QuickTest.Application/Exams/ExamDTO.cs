using MediatR;
using QuickTest.Application.Common.Enums;
using QuickTest.Application.Questions;
using QuickTest.Application.Students;

namespace QuickTest.Application.Exams;
public sealed record ExamDTO : IRequest
{
    public int Id { get; init; }

    public string Title { get; init; }

    public int Time { get; init; }

    public DateTime AvailableFrom { get; init; }

    public DateTime AvailableTo { get; init; }

    public DateTime CreationDate { get; init; }

    public int MaxPoints { get; init; }

    public IEnumerable<QuestionDTO> Questions { get; init; }

    public IEnumerable<StudentDto> Students { get; init; }

    public ExamStatus Status { get; init; }

    public string Class { get; init; }

    public int CompletedExams { get; init; }

    public int AllExams { get; init; }
}