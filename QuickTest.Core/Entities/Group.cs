namespace QuickTest.Core.Entities;

public class Group : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Teacher? Teacher { get; set; }

    public int? TeacherId { get; set; }

    public IEnumerable<Student>? Students { get; set; }
    public School? School { get; set; }
}
