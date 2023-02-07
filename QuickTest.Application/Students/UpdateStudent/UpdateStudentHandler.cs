using MediatR;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Students.UpdateStudent;

public class UpdateStudentHandler : IRequestHandler<UpdateStudentRequest, StudentDto>
{
    private readonly IStudentRepository studentRepository;

    public UpdateStudentHandler(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository;
    }

    public async Task<StudentDto> Handle(UpdateStudentRequest request, CancellationToken cancellationToken)
    {
        var student = await this.studentRepository.GetByIdAsync(request.Student.Id.Value);

        student.FirstName = request.Student.FirstName;
        student.LastName = request.Student.LastName;
        student.Email = request.Student.Email;
        student.GroupId = request.Student.GroupId;

        await this.studentRepository.UpdateAsync(student);

        return request.Student;
    }
}
