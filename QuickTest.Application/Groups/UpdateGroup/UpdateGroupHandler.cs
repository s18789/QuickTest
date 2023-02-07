using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Groups.UpdateGroup;

public class UpdateGroupHandler : IRequestHandler<UpdateGroupRequest, GroupDto>
{
    private readonly IGroupRepository groupRepository;

    public UpdateGroupHandler(IGroupRepository groupRepository)
    {
        this.groupRepository = groupRepository;
    }

    public async Task<GroupDto> Handle(UpdateGroupRequest request, CancellationToken cancellationToken)
    {
        await this.groupRepository.UpdateAsync(new Core.Entities.Group
        {
            Id = request.Group.Id,
            Name = request.Group.Name,
        });

        return request.Group;
    }
}
