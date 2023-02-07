using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Infrastructure.Repositories;

public class StudentAnswerRepository : BaseRepository<StudentAnswer>, IStudentAnswerRepository
{
    public StudentAnswerRepository(DataContext dbContext) : base(dbContext)
    {
    }
}
