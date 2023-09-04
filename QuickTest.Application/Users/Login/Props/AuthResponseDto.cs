namespace QuickTest.Application.Users.Login.Props;

public sealed record AuthResponseDto
{
    public int UserId { get; init; }
    public bool IsAuthSuccessful { get; init; }
    public string? ErrorMessage { get; init; }
    public string? Token { get; init; }
    public int SchoolId { get; set; }
}
