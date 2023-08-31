using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Interfaces;
public interface IExamResultRepository : IAsyncRepository<ExamResult>
{
    Task<IEnumerable<ExamResult>> GetStudentExamsResults(int studentId);

    Task<ExamResult> GetExamResultToSolve(int examResultId);

    Task StartExamTime(int examResultId);

    Task<ExamResult> GetExamResultById(int examResultId);

    Task AddMembersToExam(int examId, IEnumerable<int> membersIds);

    Task<ExamResult> GetExamResultPreview(int examResultId);

    Task<ExamResult> GetExamResult(int examResultId);

    Task<IEnumerable<ExamResult>> GetCompletedExamsResults(int studentId);

    Task<IEnumerable<ExamResult>> GetExamsResultsToResolve(int studentId);

    Task<double> GetAvgScoreForExam(int examId);

    Task<IEnumerable<ExamResult>> GetExamsResultsToCheck(int teacherId);

    Task<IEnumerable<ExamResult>> GetExamsResultsForMonth(int studentId, int month, int year);

    Task<IEnumerable<ExamResult>> GetScheduleExams(User user);
}
