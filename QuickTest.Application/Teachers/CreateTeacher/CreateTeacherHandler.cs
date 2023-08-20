using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickTest.Application.Users.CreateUser;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;
using QuickTest.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.CreateTeacher
{
    public class CreateTeacherHandler : IRequestHandler<CreateTeacherRequest, TeacherDto>
    {
        public const int passwordLength = 9;

        private readonly ITeacherRepository teacherRepository;
        private readonly UserManager<User> userManager;
        private readonly IEmailService emailService;
        private readonly IUserRoleRepository roleRepository;
        private readonly IMapper mapper;

        public CreateTeacherHandler(ITeacherRepository teacherRepository, UserManager<User> userManager, IMapper mapper, IEmailService emailService, IUserRoleRepository userRoleRepository)
        {
            this.teacherRepository = teacherRepository;
            this.userManager = userManager;
            this.mapper = mapper;
            this.emailService = emailService;
            this.roleRepository = userRoleRepository;
        }

        public async Task<TeacherDto> Handle(CreateTeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = mapper.Map<Teacher>(request.Teacher);
            teacher.UserName = request.Teacher.Email.Split('@')[0];
            teacher.NormalizedEmail = request.Teacher.Email.ToUpper();

            teacher.UserRoleId = roleRepository.GetRoleByName("teacher").Result.Id;

            await this.teacherRepository.AddAsync(teacher);

            var generatedPassword = UserUtilities.GenerateRandomPassword();

            var passwordResult = await this.userManager.AddPasswordAsync(teacher, generatedPassword);

            if (!passwordResult.Succeeded)
            {
                throw new Exception("Failed to set password for the teacher.");
            }

            var emailResult = await emailService.SendEmailAsync(teacher.Email, "Your Account Password", $"Your generated password is: {generatedPassword}. Please change it upon first login.");

            if (!emailResult)
            {
                throw new Exception("Failed to send email to the teacher.");
            }

            return mapper.Map<TeacherDto>(teacher);
        }
    }
}
