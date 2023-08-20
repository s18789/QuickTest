using Microsoft.AspNetCore.Identity;

namespace QuickTest.Utilities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
