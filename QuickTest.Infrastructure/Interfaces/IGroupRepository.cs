using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Interfaces;

public interface IGroupRepository : IAsyncRepository<Group>
{
    Task<bool> CheckIfGroupExists(string groupName);
    Task<Group> CreateGropuByName(string groupName, School school);
    Task DetachThatMfcker(Group group);
    Task AddGroupTeacher(GroupTeacher group);


}
