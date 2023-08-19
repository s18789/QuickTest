using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Students.GetStudentsByGroup
{
    public class GetStudentsByGroupRequest : IRequest<IEnumerable<StudentDto>>
    {
        public int GroupId { get; set; }
    }
}
