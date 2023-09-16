using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Interfaces;

public interface IGroupRepository : IAsyncRepository<Group>
{
    Task<IEnumerable<Group>> GetGroups();

    Task<bool> CheckIfGroupExists(string groupName);
    Task<Group> CreateGroupByName(string groupName, School school);

    Task DetachThatMfcker(Group group);
    Task DetachGroupTeacherMfcker(Group group);
    Task AddGroupTeacher(GroupTeacher group);
    Task<Group> GetGroupById(int id);
    Task<Group> GetGroupByName(string groupName);

}
