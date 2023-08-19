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
}
