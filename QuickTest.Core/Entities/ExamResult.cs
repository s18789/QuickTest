namespace QuickTest.Core.Entities;
public class ExamResult: IEntity
{
    public int Id { get; set; }

    public bool? MailSend { get; set; }

    public DateTime? StartExamTime { get; set; }

    public DateTime? FinishExamTime { get; set; }

    public double? Score { get; set; }

    public int ExamId { get; set; }

    public Exam Exam { get; set; }

    public int StudentId { get; set; }

    public Student Student { get; set; }

    public IEnumerable<StudentAnswer>? StudentAnswers { get; set; }
}
