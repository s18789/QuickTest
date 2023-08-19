using MediatR;
using QuickTest.Application.Groups;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.GetTeacher
{
    public class GetTeacherHandler : IRequestHandler<GetTeacherRequest, TeacherDto>
    {
        private readonly ITeacherRepository teacherRepository;

        public GetTeacherHandler(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        public async Task<TeacherDto> Handle(GetTeacherRequest request, CancellationToken cancellationToken)
        {
            var teacher = await this.teacherRepository.GetTeacherIncludeGroups(request.Id);

            var groupDtos = new List<GroupDto>();
            foreach (var group in teacher.Groups)
            {
                groupDtos.Add(new GroupDto
                {
                    Id = group.Id,
                    Name = group.Name
                });
            }

            return new TeacherDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                GroupDtos = groupDtos
            };
        }
    }
}
