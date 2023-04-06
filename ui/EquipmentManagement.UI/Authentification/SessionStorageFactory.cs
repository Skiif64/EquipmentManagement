using EquipmentManagement.UI.Abstractions;

namespace EquipmentManagement.UI.Authentification;

public class SessionStorageFactory : ITokenStorageFactory
{
    private readonly IServiceProvider _serviceProvider;

    public SessionStorageFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ITokenStorage CreateTokenStorage()
        => _serviceProvider.GetRequiredService<SessionTokenStorage>();
}
