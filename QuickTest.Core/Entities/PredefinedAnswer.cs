namespace QuickTest.Core.Entities;
public class PredefinedAnswer : IEntity
{
    public int Id { get; set; }

    public string Content { get; set; }

    public bool IsCorrect { get; set; }

    public Question Question { get; set; }

    public int QuestionId { get; set; }

    public IEnumerable<SelectedStudentAnswer>? SelectedStudentAnswers { get; set; }
}
