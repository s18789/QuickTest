using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTest.Core.Entities;

[Table("Students")]
public class Student : User
{
    public string Index { get; set; }

    public Group Group { get; set; }

    public int GroupId { get; set; }

    public IEnumerable<ExamResult>? ExamResults { get; set; }
}
