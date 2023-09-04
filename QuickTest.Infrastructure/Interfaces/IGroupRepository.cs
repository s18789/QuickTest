using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Interfaces;

public interface IGroupRepository : IAsyncRepository<Group>
{
    Task<bool> CheckIfGroupExists(string groupName);
    Task<Group> CreateGropuByName(string groupName, School school);
}
