using QuickTest.Application.Teachers;
using QuickTest.Core.Entities;

namespace QuickTest.Application.Groups
{
    public class GroupTeacherDto
    {
        public int? GroupId { get; set; }
        public GroupDto Group { get; set; }

        public int? TeacherId { get; set; }
        public TeacherDto Teacher { get; set; }
    }
}