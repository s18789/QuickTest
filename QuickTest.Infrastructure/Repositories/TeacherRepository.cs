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
        return await this.context.Teachers.Include(x => x.Groups).ToListAsync();
    }

    public async Task<Teacher> GetTeacherIncludeGroups(int id)
    {
        return await this.context.Teachers
            .Where(x => x.Id == id)
            .Include(x => x.Groups)
            .FirstOrDefaultAsync();
    }
}
