using MediatR;

namespace QuickTest.Application.Groups.GetGroup;

public class GetGroupRequest : IRequest<GroupDto>
{
    public int Id { get; set; }
}
