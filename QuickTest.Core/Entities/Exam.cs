namespace QuickTest.Core.Entities;
public class Exam : IEntity
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int Time { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime AvailableFrom { get; set; }

    public DateTime AvailableTo { get; set; }

    public int MaxPoints { get; set; }
    public string Description { get; set; }

    public Teacher Teacher { get; set; }

    public int TeacherId { get; set; }

    public IEnumerable<Question> Questions { get; set; }

    public IEnumerable<ExamResult> ExamResults { get; set; }
}
