using QuickTest.Core.Entities.Enums;

namespace QuickTest.Core.Entities;
public class Question : IEntity
{
    public int Id { get; set; }

    public string QuestionContent { get; set; }

    public double Points { get; set; }

    public QuestionType Type { get; set; }

    public int ExamId { get; set; }
    public string? Description { get; set; }
    public string? Title { get; set; }

    public Exam Exam { get; set; }

    public IEnumerable<PredefinedAnswer>? PredefinedAnswers { get; set; }

    public IEnumerable<StudentAnswer>? StudentAnswers { get; set; }
}
