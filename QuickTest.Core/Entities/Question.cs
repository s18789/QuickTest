namespace QuickTest.Core.Entities;
public class Question : IEntity
{
    public int Id { get; set; }

    public string QuestionContent { get; set; }

    public double Points { get; set; }

    public int ExamId { get; set; }

    public Exam Exam { get; set; }

    public IEnumerable<PredefinedAnswer> PredefinedAnswers { get; set; }
}
