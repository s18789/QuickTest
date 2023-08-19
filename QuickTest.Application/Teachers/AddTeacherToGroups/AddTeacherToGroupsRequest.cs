using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.AddTeacherToGroups
{
    public class AddTeacherToGroupsRequest : IRequest<bool>
    {
        public int TeacherId { get; set; }
        public List<int> GroupIds { get; set; }
    }
}
