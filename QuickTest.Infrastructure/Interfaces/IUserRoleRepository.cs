using QuickTest.Core.Entities;
using QuickTest.Core.Entities.Enums;

namespace QuickTest.Infrastructure.Interfaces;

public interface IUserRoleRepository : IAsyncRepository<UserRole>
{
    Task<UserRole> GetRoleAsync(User user);
    Task<UserRole> GetRoleByName(string roleName);
    Task<UserRole> GetRoleByType(RoleType roleType);
    Task AttachUserRole(UserRole userRole); 

}
