using MediatR;

namespace QuickTest.Application.Students.UpdateStudent;

public class UpdateStudentRequest : IRequest<StudentDto>
{
    public StudentDto Student { get; set; }
}
