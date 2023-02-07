using Microsoft.AspNetCore.Identity;

namespace QuickTest.Core.Entities;

public class UserRole : IdentityRole, IEntity
{
    public int Id { get; set; }

    public IEnumerable<User> Users { get; set; }
}
