using MediatR;

namespace QuickTest.Application.Students.CreateStudent;

public class CreateStudentRequest : IRequest<StudentDto>
{
    public StudentDto Student { get; set; }
}
