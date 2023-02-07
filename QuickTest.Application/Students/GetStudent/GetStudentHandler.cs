using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Students.GetStudent;

public class GetStudentHandler : IRequestHandler<GetStudentRequest, StudentDto>
{
    private readonly IStudentRepository studentRepository;

    public GetStudentHandler(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository;
    }

    public async Task<StudentDto> Handle(GetStudentRequest request, CancellationToken cancellationToken)
    {
        var student = await this.studentRepository.GetStudentIncludeGroup(request.Id);

        return new StudentDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            GroupId = student.Group.Id,
            GroupDto = new Groups.GroupDto
            {
                Id = student.Group.Id,
                Name = student.Group.Name
            }
        };
    }
}
