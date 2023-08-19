using AutoMapper;
using MediatR;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Groups.CreateGroup;

public class CreateGroupHandler : IRequestHandler<CreateGroupRequest, GroupDto>
{
    private readonly IGroupRepository groupRepository;
    private readonly IMapper mapper;

    public CreateGroupHandler(IGroupRepository groupRepository, IMapper mapper)
    {
        this.groupRepository = groupRepository;
        this.mapper = mapper;
    }

    public async Task<GroupDto> Handle(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var group = mapper.Map<Group>(request.Group);

        if (group.GroupTeachers?.Any() == true)
        {
            return null;
        }

        await this.groupRepository.AddAsync(group);

        return mapper.Map<GroupDto>(group);
    }
}
