using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.Users.Login;
using QuickTest.Application.Users.Login.Props;

namespace QuickTest.Controllers;
[Route("api/[controller]")]
[Authorize(Roles = "teacher")]
[ApiController]

public class UsersController : ControllerBase
{
    private readonly IMediator mediator;

    public UsersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
    {
        var response = await this.mediator.Send(new LoginRequest() { User = userForAuthentication });

        if (!response.IsAuthSuccessful)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }
}
