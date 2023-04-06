using EquipmentManagement.UI.Abstractions;

namespace EquipmentManagement.UI.Authentification;

public class LocalTokenStorageFactory : ITokenStorageFactory
{
    private readonly IServiceProvider _serviceProvider;

    public LocalTokenStorageFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ITokenStorage CreateTokenStorage() 
        => _serviceProvider.GetRequiredService<LocalTokenStorage>();
    
}
