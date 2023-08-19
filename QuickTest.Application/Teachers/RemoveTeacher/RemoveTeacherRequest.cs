using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.RemoveTeacher
{
    public class RemoveTeacherRequest : IRequest<bool>
    {
        public int TeacherId { get; set; }
    }
}
