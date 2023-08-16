using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.GetTeacher
{
    public class GetTeacherRequest: IRequest<TeacherDto>
    {
        public int Id { get; set; }
    }
}
