using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTest.Core.Entities;

[Table("Users")]
public abstract class User : IdentityUser<int>
{
    
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public int UserRoleId { get; set; }

    public UserRole UserRole { get; set; }
}
