using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Infrastructure.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public StudentRepository(DataContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Student>> GetStudentsWithGroup()
    {
        return await this.context.Students.Include(x => x.Group).ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetStudentsWithGroupForExam(int examId)
    {
        return await this.context.ExamResults
            .Where(x => x.ExamId == examId)
            .Include(x => x.Student)
                .ThenInclude(x => x.Group)
            .Select(x => x.Student)
            .ToListAsync();
    }

    public async Task<Student> GetStudentIncludeGroup(int id)
    {
        return await this.context.Students
            .Where(x => x.Id == id)
            .Include(x => x.Group)
            .FirstOrDefaultAsync();
    }
}
