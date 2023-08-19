using MediatR;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.RemoveTeacher
{
    public class RemoveTeacherHandler : IRequestHandler<RemoveTeacherRequest, bool>
    {
        private readonly ITeacherRepository teacherRepository;

        public RemoveTeacherHandler(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        public async Task<bool> Handle(RemoveTeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = await this.teacherRepository.GetByIdAsync(request.TeacherId);

            if (teacher == null)
            {
                return false;
            }

            await this.teacherRepository.DeleteAsync(teacher);

            return true;
        }
    }
}
