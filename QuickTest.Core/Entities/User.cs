using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickTest.Core.Entities;

[Table("Users")]
public abstract class User : IdentityUser, IEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int UserRoleId { get; set; }

    public UserRole UserRole { get; set; }
}
