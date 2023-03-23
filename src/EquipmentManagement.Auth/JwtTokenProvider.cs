using EquipmentManagement.Application.Models;
using EquipmentManagement.Auth.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EquipmentManagement.Auth;

public class JwtTokenProvider : IJwtTokenProvider
{
    public string Generate(ApplicationUser user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key"));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Login),
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
}
