using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickTest.Application.Users.Login.Props;
using QuickTest.Application.Users.Login.Services;
using QuickTest.Core.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace QuickTest.Application.Users.Login;

public class LoginHandler : IRequestHandler<LoginRequest, AuthResponseDto>
{
    private readonly UserManager<User> userManager;
    private readonly JwtHandler jwtHandler;

    public LoginHandler(UserManager<User> userManager, JwtHandler jwtHandler)
    {
        this.userManager = userManager;
        this.jwtHandler = jwtHandler;
    }

    public async Task<AuthResponseDto> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.FindByEmailAsync(request.User.Email);

        if (user == null || !await userManager.CheckPasswordAsync(user, request.User.Password))
        {
            return new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = "Entered incorrect email or password." };
        }

        var signingCredentials = this.jwtHandler.GetSigningCredentials();
        var claims = await this.jwtHandler.GetClaims(user);
        var schoolId = await this.jwtHandler.GetSchoolId(user);
        var tokenOptions = this.jwtHandler.GenerateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new AuthResponseDto { IsAuthSuccessful = true, Token = token, UserId = user.Id , SchoolId = schoolId};
    }
    
}
