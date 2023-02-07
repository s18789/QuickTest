using MediatR;

namespace QuickTest.Application.Students.GetStudents;

public class GetStudentsRequest : IRequest<IEnumerable<StudentDto>>
{
}
