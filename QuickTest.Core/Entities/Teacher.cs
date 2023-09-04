using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTest.Core.Entities;

[Table("Teachers")]
public class Teacher : User
{
    public ICollection<GroupTeacher>? GroupTeachers { get; set; }

    public IEnumerable<Exam>? Exams { get; set; }
}
