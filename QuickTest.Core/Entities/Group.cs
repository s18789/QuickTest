namespace QuickTest.Core.Entities;

public class Group : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Student>? Students { get; set; }

    public ICollection<GroupTeacher> GroupTeachers { get; set; }

    public School? School { get; set; }
}
