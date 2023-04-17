using EquipmentManagement.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EquipmentManagement.WebApi.OptionSetups;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtTokenOptions _options;
    
    public JwtBearerOptionsSetup(IOptions<JwtTokenOptions> jwtOptions)
    {
        _options = jwtOptions.Value;        
    }

    public void Configure(JwtBearerOptions options)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_options.SecretKey);
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = false,
            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)            
        };        
    }
}
