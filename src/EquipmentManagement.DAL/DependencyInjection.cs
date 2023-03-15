using EquipmentManagement.DAL.Migrations;
using EquipmentManagement.DAL.Repositories;
using EquipmentManagement.Domain.Abstractions.Repositories;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace EquipmentManagement.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Data");
        
        services.AddTransient<IDbConnectionFactory, PostgreSqlConnectionFactory>();
        services.AddScoped<IDbConnection>(sp => sp.GetRequiredService<IDbConnectionFactory>().Create(connectionString));

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}
