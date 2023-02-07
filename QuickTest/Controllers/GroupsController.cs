using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickTest.Application.Groups;
using QuickTest.Application.Groups.CreateGroup;
using QuickTest.Application.Groups.GetGroup;
using QuickTest.Application.Groups.GetGroups;
using QuickTest.Application.Groups.UpdateGroup;

namespace QuickTest.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "Teacher")]
[ApiController]
public class GroupsController : ControllerBase
{
    private readonly IMediator mediator;

    public GroupsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        var groups = await this.mediator.Send(new GetGroupsRequest());

        return this.Ok(groups);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGroup(int id)
    {
        var group = await this.mediator.Send(new GetGroupRequest() { Id = id });

        return this.Ok(group);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GroupDto group)
    {
        await this.mediator.Send(new CreateGroupRequest() { Group = group });

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] GroupDto group)
    {
        await this.mediator.Send(new UpdateGroupRequest() { Group = group });

        return Ok();
    }

}
