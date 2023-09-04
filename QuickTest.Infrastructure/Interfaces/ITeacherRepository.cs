using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Interfaces;

public interface ITeacherRepository : IAsyncRepository<Teacher>
{
    Task<Teacher> GetTeacherIncludeGroups(int id);
    Task<IEnumerable<Teacher>> GetTeachersWithGroups();
    Task<bool> CheckIfTeacherExists(string email);
}
