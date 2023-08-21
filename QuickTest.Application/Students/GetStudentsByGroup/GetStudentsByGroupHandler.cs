using QuickTest.Application.Groups;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Students.GetStudentsByGroup
{
    public class GetStudentsByGroupHandler
    {
        private readonly IStudentRepository studentRepository;

        public GetStudentsByGroupHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<IEnumerable<StudentDto>> Handle(GetStudentsByGroupRequest request, CancellationToken cancellationToken)
        {
            var students = await this.studentRepository.GetStudentsByGroupId(request.GroupId);

            return students.Select(x => new StudentDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Group = new GroupDto
                {
                    Id = x.Group.Id,
                    Name = x.Group.Name
                }
            });
        }
    }
}
