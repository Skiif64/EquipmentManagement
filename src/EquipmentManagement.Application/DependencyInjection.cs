using EquipmentManagement.Application.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EquipmentManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ICommand).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
