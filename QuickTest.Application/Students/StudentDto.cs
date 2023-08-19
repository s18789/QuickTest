using QuickTest.Application.Groups;
using System.ComponentModel.DataAnnotations;

namespace QuickTest.Application.Students;
public sealed record StudentDto
{
    public int? Id { get; init; }


    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }


    public GroupDto? Group { get; init; }
}
