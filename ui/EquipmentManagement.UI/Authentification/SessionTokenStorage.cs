using Blazored.LocalStorage;
using Blazored.SessionStorage;
using EquipmentManagement.UI.Abstractions;

namespace EquipmentManagement.UI.Authentification;

public class SessionTokenStorage : ITokenStorage
{
    private const string AccessToken = "JwtAccess";
    private const string RefreshToken = "JwtRefresh";
    private readonly ISessionStorageService _storage;

    public SessionTokenStorage(ISessionStorageService storage)
    {
        _storage = storage;
    }

    public async Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    => await _storage.GetItemAsStringAsync(AccessToken, cancellationToken);

    public async Task<string?> GetRefreshTokenAsync(CancellationToken cancellationToken = default)
    => await _storage.GetItemAsStringAsync(RefreshToken, cancellationToken);

    public async Task RemoveAccessTokenAsync(CancellationToken cancellationToken)
    => await _storage.RemoveItemAsync(AccessToken, cancellationToken);

    public async Task RemoveRefreshTokenAsync(CancellationToken cancellationToken)
        => await _storage.RemoveItemAsync(RefreshToken, cancellationToken);

    public async Task SetAccessTokenAsync(string token, CancellationToken cancellationToken = default)
        => await _storage.SetItemAsStringAsync(AccessToken, token, cancellationToken);


    public async Task SetRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
        => await _storage.SetItemAsStringAsync(RefreshToken, token, cancellationToken);
}
