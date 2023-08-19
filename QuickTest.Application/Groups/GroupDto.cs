using QuickTest.Application.Students;
using QuickTest.Application.Teachers;

namespace QuickTest.Application.Groups;

public class GroupDto
{
    public int Id { get; set; }

    public string Name { get; set; }
    public ICollection<StudentDto> StudentDtos { get; set; }
    public ICollection<TeacherDto> TeacherDtos { get; set; }
}
