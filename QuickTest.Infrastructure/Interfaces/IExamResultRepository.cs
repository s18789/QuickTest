using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Interfaces;
public interface IExamResultRepository : IAsyncRepository<ExamResult>
{
    Task<IEnumerable<ExamResult>> GetStudentExamsResults(int studentId);

    Task<ExamResult> GetExamResultToSolve(int examResultId);

    Task StartExamTime(int examResultId);

    Task<ExamResult> GetExamResultById(int examResultId);
}
