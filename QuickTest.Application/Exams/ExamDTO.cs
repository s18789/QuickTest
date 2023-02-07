using MediatR;
using QuickTest.Application.Questions;
using QuickTest.Application.Students;

namespace QuickTest.Application.Exams;
public class ExamDTO : IRequest
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int Time { get; set; }

    public DateTime AvailableFrom { get; set; }

    public DateTime AvailableTo { get; set; }

    public DateTime CreationDate { get; set; }

    public int MaxPoints { get; set; }

    public IEnumerable<QuestionDTO> Questions { get; set; }

    public IEnumerable<StudentDto> Students { get; set; }

    public string Status { get; set; }

    public string Class { get; set; }

    public int CompletedExams { get; set; }

    public int AllExams { get; set; }
}