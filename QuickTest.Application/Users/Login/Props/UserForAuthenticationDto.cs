using System.ComponentModel.DataAnnotations;

namespace QuickTest.Application.Users.Login.Props;

public class UserForAuthenticationDto
{
    [Required(ErrorMessage = "Email is required.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }
}
