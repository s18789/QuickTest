using QuickTest.Application.Schools;
using QuickTest.Application.Students;

namespace QuickTest.Application.Groups;

public sealed record GroupDto
{
    public int Id { get; init; }

    public string? Name { get; set; }

    public int? StudentsCount { get; set; }

    public ICollection<StudentDto>? Students { get; set; }

    public ICollection<GroupTeacherDto>? GroupTeachers { get; set; }

    public SchoolDto? School { get; set; }

}
