using QuickTest.Application.Groups;

namespace QuickTest.Application.Students;
public sealed record StudentDto
{
    public int? Id { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; }

    public GroupDto? Group { get; init; }
}
