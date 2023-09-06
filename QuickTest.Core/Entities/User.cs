using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuickTest.Core.Entities;

[Table("Users")]
public abstract class User : IdentityUser<int>
{
    
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public int UserRoleId { get; set; }
    [JsonIgnore]
    public UserRole? UserRole { get; set; }
}
