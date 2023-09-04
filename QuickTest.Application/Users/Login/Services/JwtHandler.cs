using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuickTest.Application.Users.Login.Services;

public class JwtHandler
{
    private readonly IConfiguration configuration;
    private readonly IConfigurationSection jwtSettings;
    private readonly IUserRoleRepository userRoleRepository;
    private readonly ISchoolRepository schoolRepository;
    public JwtHandler(IConfiguration configuration, IUserRoleRepository userRoleRepository, ISchoolRepository schoolRepository)
    {
        this.configuration = configuration;
        jwtSettings = this.configuration.GetSection("JwtSettings");
        this.userRoleRepository = userRoleRepository;
        this.schoolRepository = schoolRepository;
    }

    public SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<IEnumerable<Claim>> GetClaims(User user)
    {
        var userRole = await userRoleRepository.GetRoleAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, userRole.Name),
        };

        return claims;
    }
    public async Task<int> GetSchoolId(User user)
    {
        if (user.UserRole.Name != "administrator")
        {
            return 0;
        }
        else
        {
            return await schoolRepository.GetSchoolIdByAdministrator(user); 
        }
        
    }

    public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiryInMinutes"])),
            signingCredentials: signingCredentials);

        return tokenOptions;
    }
}
