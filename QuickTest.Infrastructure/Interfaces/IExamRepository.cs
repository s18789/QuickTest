using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Interfaces;
public interface IExamRepository : IAsyncRepository<Exam>
{
    Task<IEnumerable<Exam>> GetAllExams();

    Task<Exam> GetExamIncludeExamResultsAndQuestions(int id);

    Task<Exam> GetExamPreview(int id);
}