using MediatR;
using QuickTest.Application.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.UpdateTeacher
{
    public class UpdateTeacherRequest : IRequest<TeacherDto>
    {
        public TeacherDto Teacher { get; set; }
    }
}
