using MediatR;
using QuickTest.Application.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.CreateTeacher
{
    public class CreateTeacherRequest : IRequest<TeacherDto>
    {
        public TeacherDto Teacher { get; set; }
    }
}
