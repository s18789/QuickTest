using QuickTest.Application.Questions;
using QuickTest.Application.Students;
using QuickTest.Application.Teachers;
using QuickTest.Core.Entities;

namespace QuickTest.Application.Exams.CreateExam;

public sealed record CreateExamDto
{
    public string Title { get; init; }

    public int Time { get; init; }

    public DateTime AvailableFrom { get; init; }

    public DateTime AvailableTo { get; init; }
    public TeacherDto Teacher { get; init; }

    public IEnumerable<QuestionDTO> Questions { get; init; }

    public IEnumerable<StudentDto> Students { get; init; }
}


