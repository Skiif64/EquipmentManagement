using EquipmentManagement.Auth.Models;
using EquipmentManagement.Domain.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EquipmentManagement.Auth;

internal class JwtAuthentificationProvider
{
    private readonly UserStoreDbContext _userStoreDbContext;

    public JwtAuthentificationProvider(UserStoreDbContext userStoreDbContext)
    {
        _userStoreDbContext = userStoreDbContext;
    }

    private string CreateJWT(IdentityUser user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key"));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            audience: "audience",
            issuer: "issuer",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
            );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);        
    }

    public async Task<bool> SigninAsync(IdentityUser user, string password, CancellationToken cancellationToken)
    {

    }
}
