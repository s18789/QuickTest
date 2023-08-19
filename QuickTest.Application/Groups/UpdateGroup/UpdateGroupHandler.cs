using AutoMapper;
using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Groups.UpdateGroup;

public class UpdateGroupHandler : IRequestHandler<UpdateGroupRequest, GroupDto>
{
    private readonly IGroupRepository groupRepository;
    private readonly IMapper mapper;

    public UpdateGroupHandler(IGroupRepository groupRepository, IMapper mapper)
    {
        this.groupRepository = groupRepository;
        this.mapper = mapper;
    }

    public async Task<GroupDto> Handle(UpdateGroupRequest request, CancellationToken cancellationToken)
    {
        var existingGroup = await groupRepository.GetByIdAsync(request.Group.Id);
        if (existingGroup == null)
        {
            return null;
        }

        mapper.Map(request.Group, existingGroup);

        if (existingGroup.Students?.Any() == true && (existingGroup.GroupTeachers == null || !existingGroup.GroupTeachers.Any()))
        {
            return null;
        }

        await this.groupRepository.UpdateAsync(existingGroup);

        return mapper.Map<GroupDto>(existingGroup);
    }
}
