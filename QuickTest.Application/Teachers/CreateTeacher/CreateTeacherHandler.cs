using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;
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
        private readonly IMapper mapper;

        public CreateTeacherHandler(ITeacherRepository teacherRepository, UserManager<User> userManager, IMapper mapper)
        {
            this.teacherRepository = teacherRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<TeacherDto> Handle(CreateTeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = mapper.Map<Teacher>(request.Teacher);
            teacher.UserName = $"{request.Teacher.FirstName}{request.Teacher.LastName}";
            teacher.NormalizedEmail = request.Teacher.Email.ToUpper();
            teacher.UserRoleId = 1;  

            await this.teacherRepository.AddAsync(teacher);

            var passwordResult = await this.userManager.AddPasswordAsync(teacher, "quickTest123!");

            if (!passwordResult.Succeeded)
            {
                throw new Exception("Failed to set password for the teacher.");  
            }

            return mapper.Map<TeacherDto>(teacher);
        }
    }
}
