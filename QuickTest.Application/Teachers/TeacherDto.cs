using QuickTest.Application.Groups;
using QuickTest.Application.Users.UserRole;

namespace QuickTest.Application.Teachers
{
    public class TeacherDto
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string? UserName { get; set; }

        public UserRoleDto? UserRole { get; set; }

        public ICollection<GroupDto>? Group { get; set; }
    }
}
