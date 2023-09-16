using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Interfaces;

public interface IStudentRepository : IAsyncRepository<Student>
{
    Task<IEnumerable<Student>> GetStudentsWithGroup();
    Task<IEnumerable<Student>> GetStudentsByGroupId(int GroupId);

    Task<Student> GetStudentIncludeGroup(int id);
    Task<Student> GetStudentByEmail(string email);

    Task<IEnumerable<Student>> GetStudentsWithGroupForExam(int examId);
    Task<string> GenerateStudentIndex();
    Task<bool> CheckIfStudentExists(string email);
}
