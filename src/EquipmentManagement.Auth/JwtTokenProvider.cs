using EquipmentManagement.Application.Models;
using EquipmentManagement.Auth.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EquipmentManagement.Auth;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly JwtTokenOptions _options;

    public JwtTokenProvider(IOptions<JwtTokenOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(ApplicationUser user)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_options.SecretKey);        
        var secretKey = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            audience: _options.Audience,
            issuer: _options.Issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.TokenLifetimeMinutes),
            signingCredentials: credentials
            );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
