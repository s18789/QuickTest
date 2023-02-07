using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Infrastructure.Repositories;

public class PredefinedAnswerRepository : BaseRepository<PredefinedAnswer>, IPredefinedAnswerRepository
{
    public PredefinedAnswerRepository(DataContext dbContext) : base(dbContext)
    {
    }
}
