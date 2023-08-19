using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Students.AddStudentToGroup
{
    public class AddStudentToGroupsRequest : IRequest<bool>
    {
        public int StudentId { get; set; }
        public List<int> GroupIds { get; set; }
    }
}
