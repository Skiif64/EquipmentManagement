using EquipmentManagement.DAL.Repositories;
using EquipmentManagement.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace EquipmentManagement.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Data");        
       
        services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}
