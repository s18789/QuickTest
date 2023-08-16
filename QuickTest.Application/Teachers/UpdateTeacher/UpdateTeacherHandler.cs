using MediatR;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.UpdateTeacher
{
    public class UpdateTeacherHandler : IRequestHandler<UpdateTeacherRequest, TeacherDto>
    {
        private readonly ITeacherRepository teacherRepository;

        public UpdateTeacherHandler(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        public async Task<TeacherDto> Handle(UpdateTeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = await this.teacherRepository.GetByIdAsync(request.Teacher.Id.Value);

            teacher.FirstName = request.Teacher.FirstName;
            teacher.LastName = request.Teacher.LastName;
            teacher.Email = request.Teacher.Email;
            
            await this.teacherRepository.UpdateAsync(teacher);

            return request.Teacher;
        }
    }
}
