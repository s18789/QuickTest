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
        if (group == null)
        {
            return BadRequest("Invalid group data.");
        }

        if (group.StudentDtos?.Any() == true && group.TeacherDtos?.FirstOrDefault() == null)
        {
            return BadRequest("Cannot add students to a group without a teacher.");
        }

        var createdGroup = await this.mediator.Send(new CreateGroupRequest() { Group = group });

        if (createdGroup == null)
        {
            return BadRequest("Failed to create the group.");
        }

        return Ok(createdGroup);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] GroupDto group)
    {
        if (group == null || group.Id == null)
        {
            return BadRequest("Invalid group data.");
        }

        if (group.StudentDtos?.Any() == true && group.TeacherDtos?.FirstOrDefault() == null)
        {
            return BadRequest("Cannot add students to a group without a teacher.");
        }

        var updatedGroup = await this.mediator.Send(new UpdateGroupRequest() { Group = group });

        if (updatedGroup == null)
        {
            return BadRequest("Failed to update the group.");
        }

        return Ok(updatedGroup);
    }

}
