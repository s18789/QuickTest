using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using QuickTest.Core.Entities.Enums;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Infrastructure.Repositories;

public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(DataContext context) : base(context)
    {
    }

    public async Task<UserRole> GetRoleAsync(User user)
    {
        return await this.context.Users
            .AsNoTracking()
            .Include(u => u.UserRole)
            .Where(u => u.Id == user.Id)
            .Select(u => u.UserRole)
            .FirstOrDefaultAsync();
    }
    public async Task<UserRole> GetRoleByName(string roleName)
    {
        return await this.context.UserRoles
            .AsNoTracking()
            .Where(ur => ur.RoleName == roleName.ToLower())
            .FirstOrDefaultAsync();
    }
    public async Task<UserRole> GetRoleByType(RoleType roleType)
    {
        string roleName = roleType.ToString().ToLower();
        return await this.context.UserRoles
            .AsNoTracking()
            .Where(ur => ur.RoleName == roleName)
            .FirstOrDefaultAsync();
    }
    public async Task AttachUserRole(UserRole userRole)
    {
        if (context.Entry(userRole).State == EntityState.Detached)
        {
            context.Attach(userRole);
        }
    }
}
