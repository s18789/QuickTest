using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickTest.Application.Students;
using QuickTest.Application.Teachers;
using QuickTest.Core.Entities;
using QuickTest.Core.Entities.Enums;
using QuickTest.Infrastructure.Interfaces;
using QuickTest.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Users.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, ResponseDto>
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly IUserRoleRepository roleRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IMapper _mapper;


        public CreateUserHandler(IMapper mapper,UserManager<User> userManager, IEmailService emailService, IUserRoleRepository roleRepository, ITeacherRepository teacherRepository, IStudentRepository studentRepository)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.roleRepository = roleRepository;
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this._mapper = mapper;
        }

        public async Task<ResponseDto> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = new CreateUserDto
            {
                UserName = request.UserDto.Email.Split('@')[0],
                Email = request.UserDto.Email,
                FirstName = request.UserDto.FirstName,
                LastName = request.UserDto.LastName,
                UserRole = request.UserDto.UserRole
                
        };
            var newTeacher = new Teacher();
            var newStudent = new Student();
            var generatedPassword = GenerateRandomPassword();
            var isCreateSuccessful = false;
            try
            {
                if (user.UserRole.Name == "teacher")
                {
                    newTeacher.FirstName = user.FirstName;
                    newTeacher.LastName = user.LastName;
                    newTeacher.Email = user.Email;
                    newTeacher.UserName = user.UserName;
                    newTeacher.NormalizedEmail = request.UserDto.Email.ToUpper();
                    newTeacher.UserRole = roleRepository.GetRoleByName("teacher").Result;

                    await this.teacherRepository.AddAsync(newTeacher);

                    var result = await userManager.AddPasswordAsync(newTeacher, generatedPassword);
                    isCreateSuccessful = result.Succeeded;
                }
                else if (user.UserRole.Name == "student")
                {
                    newStudent.FirstName = user.FirstName;
                    newStudent.LastName = user.LastName;
                    newStudent.Email = user.Email;
                    newStudent.UserName = user.UserName;
                    newStudent.UserRole = roleRepository.GetRoleByName("student").Result;
                    

                    await this.studentRepository.AddAsync(newStudent);

                    var result = await userManager.AddPasswordAsync(newStudent, generatedPassword);
                    isCreateSuccessful = result.Succeeded;

                }
                else
                {
                    return new ResponseDto { IsSuccess = false, ErrorMessage = "Such role was not found, creating user failed." };
                }

            }
            catch (Exception ex)
            {

                return new ResponseDto { IsSuccess = false, ErrorMessage = "Creating user failed. There was an exception: "+ ex.Message };
            }

            

            

            if (!isCreateSuccessful)
            {
                return new ResponseDto { IsSuccess = false, ErrorMessage = "Error creating user"};
            }

            var emailResult = await emailService.SendEmailAsync(user.Email, "Your Account Password", $"Your generated password is: {generatedPassword}. Please change it upon first login.");

            return new ResponseDto { IsEmailSent = emailResult, IsSuccess = true, AddedStudent = _mapper.Map<StudentDto>(newStudent), AddedTeacher = _mapper.Map<TeacherDto>(newTeacher) };
        }

        private string GenerateRandomPassword()
        {
            Random random = new Random();

            char randomUppercaseLetter = (char)random.Next(65, 91);

            char[] specialCharacters = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '_', '{', '}', '[', ']', '|', '\\', ':', ';', '<', '>', '?', '/', '.' };
            char randomSpecialCharacter = specialCharacters[random.Next(0, specialCharacters.Length)];

            string[] words = { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew" };
            string randomWord = words[random.Next(0, words.Length)];

            int randomNumber = random.Next(0, 10);

            string password = $"{randomUppercaseLetter}{randomSpecialCharacter}{randomWord}{randomNumber}";

            return password;
        }
    }
}
