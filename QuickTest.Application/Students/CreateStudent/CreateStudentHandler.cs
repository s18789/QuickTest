using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickTest.Application.Students.CreateStudent.Services;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Students.CreateStudent;

public class CreateStudentHandler : IRequestHandler<CreateStudentRequest, StudentDto>
{
    public const int passwordLength = 9;

    private readonly IStudentRepository studentRepository;
    private readonly UserManager<User> userManager;

    public CreateStudentHandler(IStudentRepository studentRepository, UserManager<User> userManager)
    {
        this.studentRepository = studentRepository;
        this.userManager = userManager;
    }

    public async Task<StudentDto> Handle(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstName = request.Student.FirstName,
            LastName = request.Student.LastName,
            UserName = $"{request.Student.FirstName}{request.Student.LastName}",
            Email = request.Student.Email,
            NormalizedEmail = request.Student.Email,
            GroupId = request.Student.GroupId,
            UserRoleId = 2,
            Index = "s911",
            PhoneNumber = "911",
        };

        await this.studentRepository.AddAsync(student);

        //var passwordGenerator = new PasswordGenerator(passwordLength);
        //await this.userManager.AddPasswordAsync(student, passwordGenerator.GenerateRandomPassword());

        await this.userManager.AddPasswordAsync(student, "quickTest123!");

        return request.Student;
    }
}