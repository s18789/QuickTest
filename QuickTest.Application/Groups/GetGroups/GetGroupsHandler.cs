using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Groups.GetGroups;

public class GetGroupsHandler : IRequestHandler<GetGroupsRequest, IEnumerable<GroupDto>>
{
    private readonly IGroupRepository groupRepository;

    public GetGroupsHandler(IGroupRepository groupRepository)
    {
        this.groupRepository = groupRepository;
    }

    public async Task<IEnumerable<GroupDto>> Handle(GetGroupsRequest request, CancellationToken cancellationToken)
    {
        var groups = await this.groupRepository.GetGroups();

        return groups.Select(x => new GroupDto 
        {
            Id = x.Id,
            Name = x.Name,
            StudentsCount = x.Students.Count,
        });
    }
}
