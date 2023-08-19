using QuickTest.Application.Students;
using QuickTest.Application.Teachers;

namespace QuickTest.Application.Groups;

public sealed record GroupDto
{
    public int Id { get; init; }


    public string Name { get; set; }
    public ICollection<StudentDto> StudentDtos { get; set; }
    public ICollection<TeacherDto> TeacherDtos { get; set; }

}
