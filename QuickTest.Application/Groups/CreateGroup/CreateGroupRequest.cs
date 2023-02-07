using MediatR;

namespace QuickTest.Application.Groups.CreateGroup;
public class CreateGroupRequest : IRequest<GroupDto>
{
    public GroupDto Group { get; set; }
}
