using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using System.Net.Http.Json;

namespace EquipmentManagement.UI.Authentification;

public class JwtTokenRefresher : IJwtTokenRefresher
{
    private readonly IHttpClientFactory _factory;
    private readonly ITokenStorage _storage;
    private readonly IAuthenticationStateNotifier _notifier;
    private readonly ILogger<JwtTokenRefresher> _logger;

    public JwtTokenRefresher(IHttpClientFactory factory, ITokenStorage storage, IAuthenticationStateNotifier notifier, ILogger<JwtTokenRefresher> logger)
    {
        _factory = factory;
        _storage = storage;
        _notifier = notifier;
        _logger = logger;
    }

    public async Task<bool> RefreshAccessToken(CancellationToken cancellationToken)
    {
        var refreshToken = await _storage.GetRefreshTokenAsync(cancellationToken);
        if (refreshToken is null)
            return false;
        var client = _factory.CreateClient("Auth");

        var response = await client.GetFromJsonAsync<AuthentificationResponse>($"/api/auth/refresh/{refreshToken}", cancellationToken);
        if (response is null || !response.IsSuccess)
            return false;
        await _storage.SetRefreshTokenAsync(response.RefreshToken!, cancellationToken);
        await _storage.SetAccessTokenAsync(response.Token, cancellationToken);


        _notifier.Notify();
        return true;
    }
}
