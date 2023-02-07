namespace QuickTest.Core.Entities;

public class SelectedStudentAnswer : IEntity
{
    public int Id { get; set; }

    public bool? IsSelected { get; set; }

    public int? PredefinedAnswerId { get; set; }

    public PredefinedAnswer? PredefinedAnswer { get; set; }

    public StudentAnswer? StudentAnswer { get; set; }

    public int? StudentAnswerId { get; set; }
}
