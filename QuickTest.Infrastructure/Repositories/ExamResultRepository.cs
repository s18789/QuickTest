using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Infrastructure.Repositories;

public class ExamResultRepository : BaseRepository<ExamResult>, IExamResultRepository
{
    public ExamResultRepository(DataContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<ExamResult>> GetStudentExamsResults(int studentId)
    {
        return await this.context.ExamResults
            .Include(x => x.Exam)
            .Include(x => x.Student)
            .Where(x => x.StudentId == studentId)
            .ToListAsync();
    }

    public async Task<ExamResult> GetExamResultToSolve(int examResultId)
    {
        return await this.context.ExamResults
            .Include(x => x.Exam)
                .ThenInclude(x => x.Questions)
                    .ThenInclude(x => x.PredefinedAnswers)
            .Where(x => x.Id == examResultId)
            .FirstOrDefaultAsync();
    }

    public async Task StartExamTime(int examResultId)
    {
        var examResult = await this.context.ExamResults
            .Where(x => x.Id == examResultId)
            .FirstOrDefaultAsync();

        examResult.StartExamTime = DateTime.Now;

        this.context.ExamResults.Update(examResult);
        await this.context.SaveChangesAsync();
    }

    public async Task<ExamResult> GetExamResultById(int examResultId)
    {
        var examResult = await this.context.ExamResults
            .Where(x => x.Id == examResultId)
                .Include(x => x.Exam)
                    .ThenInclude(x => x.Questions)
            .Include(x => x.StudentAnswers)
            .FirstOrDefaultAsync();

        return examResult;
    }

    public async Task AddMembersToExam(int examId, IEnumerable<int> membersIds)
    {
        this.context.ExamResults.AddRange(
            membersIds.Select(x => new ExamResult()
            {
                ExamId = examId,
                StudentId = x
            })
        );

        await this.context.SaveChangesAsync();
    }

    public async Task<ExamResult> GetExamResultPreview(int examResultId)
    {
        return await this.context.ExamResults
            .Include(x => x.Exam)
                .ThenInclude(x => x.Questions)
                    .ThenInclude(x => x.PredefinedAnswers)
                        .ThenInclude(x => x.SelectedStudentAnswers)
                            .ThenInclude(x => x.StudentAnswer)
                        .       ThenInclude(x => x.Question)
             .FirstOrDefaultAsync(x => x.Id == examResultId);


    }
}
