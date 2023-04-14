using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Journaling;
using Microsoft.Extensions.DependencyInjection;

namespace EquipmentManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ICommand).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddTransient<IDatabaseMigrator, DatabaseMigrator>();
        //services.AddScoped<IJournal, DbJournal>();
        services.AddScoped<IJournalFactory, DbJournalFactory>();
        return services;
    }
}
