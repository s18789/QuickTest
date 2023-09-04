using MediatR;
using QuickTest.Application.Students;
using QuickTest.Application.Users.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.CreateTeacher
{
    public class CreateTeacherRequest : IRequest<ResponseDto>
    {
        public TeacherDto Teacher { get; set; }
    }
}
