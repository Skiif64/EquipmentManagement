using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquipmentManagement.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Users");
        
        services.AddDbContext<UserStoreDbContext>(opt => opt.UseNpgsql(connectionString));
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<UserStoreDbContext>();
            
        return services;
    }
}
