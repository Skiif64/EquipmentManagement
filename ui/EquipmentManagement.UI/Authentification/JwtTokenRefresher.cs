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

    public async Task RefreshAccessToken(CancellationToken cancellationToken)
    {
        var refreshToken = await _storage.GetRefreshTokenAsync(cancellationToken);
        if (refreshToken is null)
            return;
        var client = _factory.CreateClient("Auth");
        AuthentificationResponse? response = null;
        try
        {
            response = await client.GetFromJsonAsync<AuthentificationResponse>($"/api/auth/refresh/{refreshToken}", cancellationToken);
            await _storage.SetRefreshTokenAsync(response.RefreshToken, cancellationToken);
            await _storage.SetAccessTokenAsync(response.Token, cancellationToken);
        }
        catch(HttpRequestException exception) when (exception.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            _logger.LogWarning("Exception Occured. Deleting tokens"); //TODO: remove
            await _storage.RemoveAccessTokenAsync(cancellationToken);
            await _storage.RemoveRefreshTokenAsync(cancellationToken);
        }
              
        
        _notifier.Notify();
    }
}
