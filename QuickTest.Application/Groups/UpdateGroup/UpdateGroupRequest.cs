using MediatR;

namespace QuickTest.Application.Groups.UpdateGroup;

public class UpdateGroupRequest : IRequest<GroupDto>
{
    public GroupDto Group { get; set; }
}
