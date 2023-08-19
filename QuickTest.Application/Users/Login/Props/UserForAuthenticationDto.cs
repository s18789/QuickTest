using System.ComponentModel.DataAnnotations;

namespace QuickTest.Application.Users.Login.Props;

public sealed record UserForAuthenticationDto
{
    [Required(ErrorMessage = "Email is required.")]
    public string? Email { get; init; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; init; }
}
