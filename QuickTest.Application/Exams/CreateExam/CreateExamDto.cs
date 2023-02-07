using QuickTest.Application.Questions;

namespace QuickTest.Application.Exams.CreateExam;

public class CreateExamDto
{
    public string Title { get; set; }

    public int Time { get; set; }

    public DateTime AvailableFrom { get; set; }

    public DateTime AvailableTo { get; set; }

    public IEnumerable<QuestionDTO> Questions { get; set; }

    public IEnumerable<Student> Students { get; set; }
}

public class Student
{
    public int Id { get; set; }
}
