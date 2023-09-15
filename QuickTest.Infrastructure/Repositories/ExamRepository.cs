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

    public async Task<IEnumerable<Exam>> GetExams(User user)
    {
        return await this.context.Exams
            .Include(x => x.ExamResults)
            .Where(x => x.TeacherId == user.Id)
            .ToListAsync();
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

    public async Task<Exam> GetExamPreview(int id)
    {
        return await this.context.Exams
            .Where(x => x.Id == id)
            .Include (x => x.Questions)
                .ThenInclude(x => x.PredefinedAnswers)
                .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Exam>> GetCreatedExam(int teacherId)
    {
        return await this.context.Exams
            .Include(e => e.ExamResults)
            .Where(e => e.TeacherId == teacherId)
            .OrderBy(e => e.CreationDate)
            .Take(5)
            .ToListAsync();
    }

    public async Task<IEnumerable<Exam>> GetExamsForMonth(int teacherId, int month, int year)
    {
        return await this.context.Exams
            .Where(x => x.AvailableTo.Month == month+1)
            .Where(x => x.AvailableTo.Year == year)
            .Where(x => x.TeacherId == teacherId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Exam>> GetExamsForMonth(int month, int year)
    {
        return await this.context.Exams
            .Where(x => x.AvailableTo.Month == month+1)
            .Where(x => x.AvailableTo.Year == year)
            .ToListAsync();
    }

    public async Task<IEnumerable<Exam>> GetScheduleExams()
    {
        return await this.context.Exams
            .Where(e => e.AvailableTo > DateTime.Now)
            .OrderBy(e => e.AvailableTo)
            .Take(3)
            .ToListAsync();
    }

    public async Task<IEnumerable<Exam>> GetScheduleExams(User user)
    {
        return await this.context.Exams
            .Where(e => e.TeacherId == user.Id)
            .Where(e => e.AvailableTo > DateTime.Now)
            .OrderBy(e => e.AvailableTo)
            .Take(3)
            .ToListAsync();
    }

    public async Task<(int, double)> FindTheHardestQuestion(int id)
    {
        var exam = await this.context.Exams
            .Include(e => e.Questions)
                .ThenInclude(q => q.PredefinedAnswers)
                    .ThenInclude(pa => pa.SelectedStudentAnswers)
                        .ThenInclude(ssa => ssa.StudentAnswer)
            .FirstOrDefaultAsync(x => x.Id == id);

        double minAverage = double.MaxValue;
        int questionIndex = 0;

        var i = 0;
        foreach (var question in exam.Questions)
        {
            var summaryScoreForAllAnswerForQuestion = question.PredefinedAnswers.FirstOrDefault().SelectedStudentAnswers.Sum(ssa => ssa.StudentAnswer.Points);
            var averageScoreForQuestion = summaryScoreForAllAnswerForQuestion / (question.Points * exam.ExamResults.Count()) * 100;
            i++;

            if (minAverage > averageScoreForQuestion)
            {
                minAverage = averageScoreForQuestion;
                questionIndex = i;
            }
        }

        return (questionIndex, minAverage);
    }
}