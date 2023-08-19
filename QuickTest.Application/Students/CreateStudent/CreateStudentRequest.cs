using MediatR;

namespace QuickTest.Application.Students.CreateStudent;

public sealed class CreateStudentRequest : IRequest<CreateStudentDTO>
{
    public CreateStudentDTO Student { get; set; }
}
