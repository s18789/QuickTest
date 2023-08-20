using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;
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

        public CreateUserHandler(UserManager<User> userManager, IEmailService emailService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
        }

        public async Task<ResponseDto> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            //var user = new Teacher
            //{
            //    UserName = request.UserDto.Email,
            //    Email = request.UserDto.Email,
            //    FirstName = request.UserDto.FirstName,
            //    LastName = request.UserDto.LastName,
            //    UserRoleId = request.UserDto.RoleId
            //};

            //var generatedPassword = GenerateRandomPassword();
            //var result = await userManager.CreateAsync(user, generatedPassword);

            //if (!result.Succeeded)
            //{
            //    return new ResponseDto { IsSuccess = false, ErrorMessage = "Error creating user" };
            //}

            //// Send email with generated password
            //var emailResult = await emailService.SendEmailAsync(user.Email, "Your Account Password", $"Your generated password is: {generatedPassword}. Please change it upon first login.");

            return new ResponseDto { IsEmailSent = true, IsSuccess = true };
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
