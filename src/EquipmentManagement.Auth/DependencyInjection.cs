using EquipmentManagement.Auth.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
        
        services.AddDbContext<UsersDbContext>(opt => opt
        .UseNpgsql(connectionString, cfg => cfg
        .MigrationsAssembly(typeof(UsersDbContext).Assembly.FullName)));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key"))
                };
            });

        services.AddTransient<IJwtTokenProvider, JwtTokenProvider>();
        services.AddScoped<JwtAuthentificationService>();
        //var provider = services.BuildServiceProvider();

        //var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
        //var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
        //UserInitializer.Initialize(roleManager, userManager, configuration).Wait();
        return services;
    }
}
