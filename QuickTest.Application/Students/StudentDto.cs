using QuickTest.Application.Groups;
using System.ComponentModel.DataAnnotations;

namespace QuickTest.Application.Students;
public class StudentDto
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public int GroupId { get; set; }

    public GroupDto? GroupDto { get; set; }
}
