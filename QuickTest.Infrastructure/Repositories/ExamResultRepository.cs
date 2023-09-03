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
            .Include(x => x.StudentAnswers)
            .Include(x => x.Exam)
                .ThenInclude(x => x.Questions)
                    .ThenInclude(x => x.PredefinedAnswers)
                        .ThenInclude(x => x.SelectedStudentAnswers)
                            .ThenInclude(x => x.StudentAnswer)
                        .       ThenInclude(x => x.Question)
             .FirstOrDefaultAsync(x => x.Id == examResultId);
    }

    public async Task<ExamResult> GetExamResult(int examResultId)
    {
        return await this.context.ExamResults.Include(x => x.StudentAnswers)
            .FirstOrDefaultAsync(x => x.Id == examResultId);
    }

    public async Task<IEnumerable<ExamResult>> GetCompletedExamsResults(int studentId)
    {
        return await this.context.ExamResults
            .Include(er => er.Exam)
            .Where(er => er.StudentId == studentId)
            .Where(er => er.FinishExamTime != null)
            .Where(er => er.Score != null)
            .OrderBy(er => er.FinishExamTime)
            .Take(5)
            .ToListAsync();
    }

    public async Task<IEnumerable<ExamResult>> GetExamsResultsToResolve(int studentId)
    {
        return await this.context.ExamResults
            .Include(er => er.Exam)
                .ThenInclude(e => e.Teacher)
            .Where(er => er.StudentId == studentId)
            .Where(er => er.FinishExamTime == null)
            .Where(er => er.Exam.AvailableFrom < DateTime.Now)
            .Where(er => er.Exam.AvailableTo > DateTime.Now)
            .OrderBy(er => er.Exam.AvailableTo)
            .Take(5)
            .ToListAsync();
    }

    public async Task<double> GetAvgScoreForExam(int examId)
    {
        var scoresList = await this.context.ExamResults
            .Include(er => er.Exam)
            .Where(er => er.ExamId == examId)
            .Where(er => er.Score != null)
            .Where(er => er.FinishExamTime != null)
            .Select(er => er.Score)
            .ToListAsync();

        return scoresList.Average().HasValue 
            ? scoresList.Average().Value
            : 0;
    }

    public async Task<IEnumerable<ExamResult>> GetExamsResultsToCheck(int teacherId)
    {
        return await this.context.ExamResults
            .Include(er => er.Student)
            .Include(er => er.Exam)
            .Where(er => er.Exam.TeacherId == teacherId)
            .Where(er => er.Score == null)
            .Where(er => er.FinishExamTime != null)
            .OrderBy(er => er.FinishExamTime)
            .Take(5)
            .ToListAsync();
    }

    public async Task<IEnumerable<ExamResult>> GetExamsResultsForMonth(int studentId, int month, int year)
    {
        return await this.context.ExamResults
            .Include(x => x.Exam)
            .Where(x => x.StudentId == studentId)
            .Where(x => x.Exam.AvailableTo.Month == month+1)
            .Where(x => x.Exam.AvailableTo.Year == year)
            .ToListAsync();
    }

    public async Task<IEnumerable<ExamResult>> GetScheduleExams(User user)
    {
        return await this.context.ExamResults
            .Include(x => x.Exam)
            .Where(er => er.StudentId == user.Id)
            .Where(er => er.FinishExamTime == null)
            .Where(er => er.Exam.AvailableTo > DateTime.Now)
            .OrderBy(er => er.Exam.AvailableTo)
            .Take(3)
            .ToListAsync();
    }
}
