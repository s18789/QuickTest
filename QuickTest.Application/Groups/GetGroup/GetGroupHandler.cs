using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Groups.GetGroup;

public class GetGroupHandler : IRequestHandler<GetGroupRequest, GroupDto>
{
    private readonly IGroupRepository groupRepository;

    public GetGroupHandler(IGroupRepository groupRepository)
    {
        this.groupRepository = groupRepository;
    }

    public async Task<GroupDto> Handle(GetGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await this.groupRepository.GetByIdAsync(request.Id);

        return new GroupDto
        {
            Id = group.Id,
            Name = group.Name
        };
    }
}
