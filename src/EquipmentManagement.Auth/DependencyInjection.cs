using EquipmentManagement.Application.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EquipmentManagement.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Users");
        var jwtOptions = new JwtTokenOptions();
        configuration.GetRequiredSection("Jwt").Bind(jwtOptions);
        services.AddDbContext<IUserDbContext, UsersDbContext>(opt => opt
        .UseNpgsql(connectionString, cfg => cfg
        .MigrationsAssembly(typeof(UsersDbContext).Assembly.FullName)));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt => opt.TokenValidationParameters = new()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                ClockSkew = TimeSpan.FromMinutes(jwtOptions.TokenLifetimeMinutes),
                LifetimeValidator = (from, to, token, parameters)
                => to > DateTime.UtcNow,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
            });

        services.AddTransient<IJwtTokenProvider, JwtTokenProvider>();        
        services.AddTransient<IMigrationableDatabase, UsersDbContext>();
        services.AddTransient<IMigrationableDatabase, DefaultAdminInitializer>();
       
        return services;
    }
}
