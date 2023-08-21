using MediatR;
using QuickTest.Application.Groups;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Students.GetStudents;

public class GetStudentsHandler : IRequestHandler<GetStudentsRequest, IEnumerable<StudentDto>>
{
    private readonly IStudentRepository studentRepository;

    public GetStudentsHandler(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository;
    }

    public async Task<IEnumerable<StudentDto>> Handle(GetStudentsRequest request, CancellationToken cancellationToken)
    {
        var students = await this.studentRepository.GetStudentsWithGroup();

        return students.Select(x => new StudentDto
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Group = new GroupDto 
            { 
                Id = x.Group.Id,
                Name = x.Group.Name
            }
        });
    }
}
