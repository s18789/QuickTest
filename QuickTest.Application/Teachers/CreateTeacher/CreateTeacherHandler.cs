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

        public CreateTeacherHandler(ITeacherRepository teacherRepository, UserManager<User> userManager)
        {
            this.teacherRepository = teacherRepository;
            this.userManager = userManager;
        }

        public async Task<TeacherDto> Handle(CreateTeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = new Teacher
            {
                FirstName = request.Teacher.FirstName,
                LastName = request.Teacher.LastName,
                UserName = $"{request.Teacher.FirstName}{request.Teacher.LastName}",
                Email = request.Teacher.Email,
                NormalizedEmail = request.Teacher.Email,
                UserRoleId = 1, 
            };

            await this.teacherRepository.AddAsync(teacher);

            
            await this.userManager.AddPasswordAsync(teacher, "quickTest123!");

            return request.Teacher;
        }
    }
}
