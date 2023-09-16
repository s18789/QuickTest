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
    public async Task<Student> GetStudentByEmail(string email)
    {
        return await this.context.Students
            .Where(x => x.Email == email)
            .Include(x => x.Group)
            .AsNoTracking()
            .FirstOrDefaultAsync();

    }
    public async Task<IEnumerable<Student>> GetStudentsByGroupId(int groupId)
    {
        return await this.context.Students
            .Where(x => x.GroupId == groupId)
            .Include(x => x.Group)
            .ToListAsync();
    }
    public async Task<string> GenerateStudentIndex()
    {
        var lastStudentIndex = await context.Students.OrderByDescending(s => s.Index).Select(s => s.Index).FirstOrDefaultAsync();

        if (string.IsNullOrEmpty(lastStudentIndex))
        {
            return "s00001";
        }

        int.TryParse(lastStudentIndex.Substring(1), out int numericPart);

        numericPart++;

        var newIndex = "s" + numericPart.ToString("D5");

        return newIndex;
    }
    public async Task<bool> CheckIfStudentExists(string email)
    {
        var userRoleId = context.UserRoles.Where(x => x.Name.Equals("student")).FirstOrDefault().Id;
        var foundUser = await this.context.Users
            .Where(t => t.UserRoleId == userRoleId && t.Email.Equals(email))
            .FirstOrDefaultAsync();
        return foundUser == null ? false : true;
    }

}
