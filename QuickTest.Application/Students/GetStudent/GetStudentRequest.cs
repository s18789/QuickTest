using MediatR;

namespace QuickTest.Application.Students.GetStudent;

public class GetStudentRequest : IRequest<StudentDto>
{
    public int Id { get; set; }
}
