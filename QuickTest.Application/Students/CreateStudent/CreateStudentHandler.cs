using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickTest.Application.Teachers;
using QuickTest.Application.Users.CreateUser;
using QuickTest.Core.Entities;
using QuickTest.Core.Entities.Enums;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Students.CreateStudent;

public class CreateStudentHandler : IRequestHandler<CreateStudentRequest, ResponseDto>
{
    public const int passwordLength = 9;

    private readonly IStudentRepository studentRepository;
    private readonly IEmailService emailService;
    private readonly UserManager<User> userManager;

    public CreateStudentHandler(IStudentRepository studentRepository, UserManager<User> userManager, IEmailService emailService)
    {
        this.studentRepository = studentRepository;
        this.userManager = userManager;
        this.emailService = emailService;
    }

    public async Task<ResponseDto> Handle(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstName = request.Student.FirstName,
            LastName = request.Student.LastName,
            UserName = request.Student.Email.Split('@')[0],
            Email = request.Student.Email,
            NormalizedEmail = request.Student.Email.ToUpper(),
            GroupId = request.Student.Group.Id,
            UserRoleId = (int)RoleType.STUDENT,
            Index = this.studentRepository.GenerateStudentIndex().Result,
        };

        try
        {
            await this.studentRepository.AddAsync(student);
        }
        catch (Exception ex)
        {
            return new ResponseDto { IsEmailSent = false, IsSuccess = false, AddedStudent = null, AddedTeacher = null, ErrorMessage = ex.ToString() };
        }

        var generatedPassword = UserUtilities.GenerateRandomPassword();
        await this.userManager.AddPasswordAsync(student, generatedPassword);

        var emailResult = await emailService.SendEmailAsync(student.Email, "Your Account Password", $"Your generated password is: {generatedPassword}. Please change it upon first login.");

        return new ResponseDto { IsEmailSent = emailResult, IsSuccess= true, AddedStudent = new StudentDto { Id = student.Id }, AddedTeacher = null};
    }
}