using System.Text.Json.Serialization;

namespace QuickTest.Core.Entities;

public class Group : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    [JsonIgnore]
    public ICollection<Student>? Students { get; set; }
    [JsonIgnore]
    public ICollection<GroupTeacher> GroupTeachers { get; set; }

    public School? School { get; set; }
}
