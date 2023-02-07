using MediatR;
using QuickTest.Application.Users.Login.Props;

namespace QuickTest.Application.Users.Login;

public class LoginRequest : IRequest<AuthResponseDto>
{
    public UserForAuthenticationDto User { get; set; }
}
