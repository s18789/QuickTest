using MediatR;
using QuickTest.Application.Groups;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.GetTeachers
{
    public class GetTeachersHandler : IRequestHandler<GetTeachersRequest, IEnumerable<TeacherDto>>
    {
        private readonly ITeacherRepository teacherRepository;

        public GetTeachersHandler(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<TeacherDto>> Handle(GetTeachersRequest request, CancellationToken cancellationToken)
        {
            var teachers = await this.teacherRepository.GetTeachers();

            return teachers.Select(x => new TeacherDto
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
            });
        }
    }
}
