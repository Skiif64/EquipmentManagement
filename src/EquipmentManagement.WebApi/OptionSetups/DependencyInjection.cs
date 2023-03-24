namespace EquipmentManagement.WebApi.OptionSetups;

public static class DependencyInjection
{
    public static IServiceCollection SetupOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtTokenOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        return services;
    }
}
