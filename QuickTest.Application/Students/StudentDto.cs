using QuickTest.Application.Groups;

namespace QuickTest.Application.Students;
public class StudentDto
{
    public int? Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public int GroupId { get; set; }

    public GroupDto? GroupDto { get; set; }
}
