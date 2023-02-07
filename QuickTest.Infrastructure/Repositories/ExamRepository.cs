using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Infrastructure.Repositories;
public class ExamRepository : BaseRepository<Exam>, IExamRepository
{
    public ExamRepository(DataContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Exam>> GetAllExams()
    {
        return await this.context.Exams.Include(x => x.ExamResults).ToListAsync();
    }

    public async Task<Exam> GetExamIncludeExamResultsAndQuestions(int id)
    {
        return await this.context.Exams
            .Where(x => x.Id == id)
            .Include(x => x.ExamResults)
                .ThenInclude(x => x.Student)
            .Include(x => x.Questions)
            .FirstOrDefaultAsync();
    }
}