using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.DAL.ImagesStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquipmentManagement.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Data");        
       
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opt => opt
        .UseNpgsql(connectionString, cfg => cfg
        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
        services.AddTransient<IMigrationableDatabase, ApplicationDbContext>();
        services.AddTransient<IImageStorage, ImageStorage>();
        return services;
    }
}
