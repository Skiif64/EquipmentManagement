using EquipmentManagement.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = _options.Audience,
            ValidateIssuer = true,
            ValidIssuer = _options.Issuer,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    }
}
