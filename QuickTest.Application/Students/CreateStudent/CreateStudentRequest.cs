using MediatR;
using QuickTest.Application.Users.CreateUser;

namespace QuickTest.Application.Students.CreateStudent;

public sealed class CreateStudentRequest : IRequest<ResponseDto>
{
    public StudentDto Student { get; set; }
}
