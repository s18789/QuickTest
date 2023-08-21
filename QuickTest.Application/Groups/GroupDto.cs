using QuickTest.Application.Students;
using QuickTest.Application.Teachers;
using QuickTest.Core.Entities;

namespace QuickTest.Application.Groups;

public sealed record GroupDto
{
    public int Id { get; init; }


    public string? Name { get; set; }

    public ICollection<StudentDto>? Students { get; set; }

    public ICollection<GroupTeacherDto>? GroupTeachers { get; set; }

    public School? School { get; set; }

}
