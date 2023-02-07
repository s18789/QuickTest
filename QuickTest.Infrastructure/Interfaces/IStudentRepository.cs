using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Interfaces;

public interface IStudentRepository : IAsyncRepository<Student>
{
    Task<IEnumerable<Student>> GetStudentsWithGroup();

    Task<Student> GetStudentIncludeGroup(int id);
}
