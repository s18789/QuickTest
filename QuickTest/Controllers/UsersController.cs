using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.Users.ChangeUserPassword;
using QuickTest.Application.Users.GetUserProfileInfo;
using QuickTest.Application.Users.Login;
using QuickTest.Application.Users.Login.Props;

namespace QuickTest.Controllers;
[Route("api/[controller]")]
[Authorize(Roles = "teacher,student,administrator")]
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

    [HttpGet("GetUserProfileInfo")]
    public async Task<IActionResult> GetUserInfoAndSchool()
    {
        var response = await this.mediator.Send(new GetUserProfileInfoRequest());

        if (response == null)
        {
            return NotFound("User or school information not found.");
        }

        return Ok(response);
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var response = await mediator.Send(new ChangePasswordRequest { ChangePasswordDto = changePasswordDto });

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
