namespace QuickTest.Application.Users.Login.Props;

public class AuthResponseDto
{
    public int UserId { get; set; }
    public bool IsAuthSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Token { get; set; }
}
