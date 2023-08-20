using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Interfaces;

public interface IUserRoleRepository : IAsyncRepository<UserRole>
{
    Task<UserRole> GetRoleAsync(User user);
    Task<UserRole> GetRoleByName(string roleName);
}
