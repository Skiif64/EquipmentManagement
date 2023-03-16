using EquipmentManagement.Auth.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EquipmentManagement.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddApiKeyAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();
        services.AddSingleton<ApiKeyAuthorizationFilter>();

        services.AddAuthentication();

        return services;
    }
}
