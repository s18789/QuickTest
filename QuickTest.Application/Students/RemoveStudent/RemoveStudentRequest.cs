using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Students.RemoveStudent
{
    public class RemoveStudentRequest : IRequest<bool>
    {
        public int StudentId { get; set; }
    }
}
