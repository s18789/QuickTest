using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
    private readonly IUserRoleRepository roleRepository;
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public CreateStudentHandler(IStudentRepository studentRepository, UserManager<User> userManager, IMapper mapper, IEmailService emailService, IUserRoleRepository userRoleRepository)
    {
        this.studentRepository = studentRepository;
        this.userManager = userManager;
        this.emailService = emailService;
        this.roleRepository = userRoleRepository;
        this.mapper = mapper;
    }

    public async Task<ResponseDto> Handle(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstName = request.Student.FirstName,
            LastName = request.Student.LastName,
            UserName = request.Student.Email.Split('@')[0],
            Email = request.Student.Email,
            NormalizedEmail = request.Student.Email,
            Group = mapper.Map<Group>(request.Student.GroupDto),
            UserRole = roleRepository.GetRoleByType(RoleType.STUDENT).Result,
            Index = studentRepository.GenerateStudentIndex().Result,
            PhoneNumber = request.Student.PhoneNumber,
        };

        await this.studentRepository.AddAsync(student);

        var generatedPassword = UserUtilities.GenerateRandomPassword();
        var result = await userManager.CreateAsync(student, generatedPassword);

        if (!result.Succeeded)
        {
            return new ResponseDto { IsSuccess = false, ErrorMessage = "Error creating student" };
        }

        var emailResult = await emailService.SendEmailAsync(student.Email, "Your Account Password", $"Your generated password is: {generatedPassword}. Please change it upon first login.");

        

        return new ResponseDto { IsEmailSent = emailResult, IsSuccess= true};
    }
}