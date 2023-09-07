using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Infrastructure.Repositories;

public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
{
    public TeacherRepository(DataContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Teacher>> GetTeachersWithGroups()
    {
        return await this.context.Teachers
            .Include(t => t.GroupTeachers) 
            .ThenInclude(gt => gt.Group) 
            .ToListAsync();
    }

    public async Task<Teacher> GetTeacherIncludeGroups(int id)
    {
        return await this.context.Teachers
            .Where(t => t.Id == id)
            .Include(t => t.GroupTeachers)
            .ThenInclude(gt => gt.Group)
            .FirstOrDefaultAsync();
    }
    public async Task<Teacher> GetTeacherWithoutGroups(int id)
    {
        return await this.context.Teachers
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();
    }
    public async Task<bool> CheckIfTeacherExists(string email)
    {
        var userRoleId = context.UserRoles.Where(x => x.Name.Equals("teacher")).FirstOrDefault().Id;
        var foundUser= await this.context.Users
            .Where(t => t.UserRoleId == userRoleId && t.Email.Equals(email))
            .FirstOrDefaultAsync();
        return foundUser == null ? false : true;
    }
}
