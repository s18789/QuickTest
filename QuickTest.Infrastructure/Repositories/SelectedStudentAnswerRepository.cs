using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Infrastructure.Repositories;

public class SelectedStudentAnswerRepository : BaseRepository<SelectedStudentAnswer>, ISelectedStudentAnswerRepository
{
    public SelectedStudentAnswerRepository(DataContext dbContext) : base(dbContext)
    {
    }
}
