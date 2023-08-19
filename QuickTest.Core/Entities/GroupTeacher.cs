using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Core.Entities
{
    public class GroupTeacher
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
