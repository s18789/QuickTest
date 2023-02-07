namespace QuickTest.Core.Entities;

public class StudentAnswer : IEntity
{
    public int Id { get; set; }

    public double Points { get; set; }

    public int ExamResultId { get; set; }

    public ExamResult ExamResult { get; set; }

    public IEnumerable<SelectedStudentAnswer>? SelectedStudentAnswers { get; set; }
}
