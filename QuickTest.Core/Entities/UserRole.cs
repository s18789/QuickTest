using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace QuickTest.Core.Entities;

public class UserRole : IdentityRole, IEntity
{
    public int Id { get; set; }
    public string RoleName { get; set; }
    [JsonIgnore]
    public IEnumerable<User> Users { get; set; }
}
