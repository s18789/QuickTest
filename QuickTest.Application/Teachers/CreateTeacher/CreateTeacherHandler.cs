using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickTest.Application.Users.CreateUser;
using QuickTest.Core.Entities;
using QuickTest.Core.Entities.Enums;
using QuickTest.Infrastructure.Interfaces;
using QuickTest.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.CreateTeacher
{
    public class CreateTeacherHandler : IRequestHandler<CreateTeacherRequest, ResponseDto>
    {
        public const int passwordLength = 9;

        private readonly ITeacherRepository teacherRepository;
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly IUserRoleRepository roleRepository;

        public CreateTeacherHandler(ITeacherRepository teacherRepository, UserManager<User> userManager, IMapper mapper, IEmailService emailService, IUserRoleRepository userRoleRepository)
        {
            this.teacherRepository = teacherRepository;
            this.userManager = userManager;
            this.emailService = emailService;
            this.roleRepository = userRoleRepository;
        }

        public async Task<ResponseDto> Handle(CreateTeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = new Teacher()
            {
                FirstName = request.Teacher.FirstName,
                LastName = request.Teacher.LastName,
                Email = request.Teacher.Email,
                UserName = request.Teacher.Email.Split('@')[0],
                NormalizedEmail = request.Teacher.Email.ToUpper(),
                UserRoleId = (int)RoleType.TEACHER,
            };


            await this.teacherRepository.AddAsync(teacher);

            var generatedPassword = UserUtilities.GenerateRandomPassword();
            var passwordResult = await this.userManager.AddPasswordAsync(teacher, generatedPassword);

            if (!passwordResult.Succeeded)
            {
                return new ResponseDto { IsEmailSent = false, IsSuccess = true, AddedStudent = null, AddedTeacher = null, ErrorMessage = "Failed to set password for the teacher. " };
            }

            var emailResult = await emailService.SendEmailAsync(teacher.Email, "Your Account Password", $"Your generated password is: {generatedPassword}. Please change it upon first login.");

            if (!emailResult)
            {
                return new ResponseDto { IsEmailSent = false, IsSuccess = true, AddedStudent = null, AddedTeacher = null, ErrorMessage = "Failed to send email to the teacher." };

            }

            return new ResponseDto { IsEmailSent = emailResult, IsSuccess = true, AddedStudent = null, AddedTeacher = new TeacherDto() { Id = teacher.Id } };
        }
    }
}
