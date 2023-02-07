using MediatR;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Groups.CreateGroup;

public class CreateGroupHandler : IRequestHandler<CreateGroupRequest, GroupDto>
{
    private readonly IGroupRepository groupRepository;

    public CreateGroupHandler(IGroupRepository groupRepository)
    {
        this.groupRepository = groupRepository;
    }

    public async Task<GroupDto> Handle(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        await this.groupRepository.AddAsync(new Group()
        {
            Name = request.Group.Name,
        });

        return request.Group;
    }
}
