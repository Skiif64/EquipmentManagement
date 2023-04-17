using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using System.Net;
using System.Net.Http.Json;

namespace EquipmentManagement.UI.Authentification;

public class JwtTokenRefresher : IJwtTokenRefresher
{
    private readonly IHttpClientFactory _factory;
    private readonly ITokenStorage _storage;
    private readonly JwtAuthentificationStateProvider _provider;
    private readonly ILogger<JwtTokenRefresher> _logger;

    public JwtTokenRefresher(IHttpClientFactory factory, ITokenStorage storage, ILogger<JwtTokenRefresher> logger, JwtAuthentificationStateProvider provider)
    {
        _factory = factory;
        _storage = storage;
        _logger = logger;
        _provider = provider;
    }

    public async Task<bool> RefreshAccessToken(CancellationToken cancellationToken)
    {
        var refreshToken = await _storage.GetRefreshTokenAsync(cancellationToken);
        if (refreshToken is null)
            return false;
        var client = _factory.CreateClient("Auth");
        var response = await client.GetAsync($"/api/auth/refresh/{refreshToken}", cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AuthentificationResponse>(
                cancellationToken: cancellationToken)
                ?? throw new Exception(); //TODO: normal exception
            await _storage.SetRefreshTokenAsync(result.RefreshToken!, cancellationToken);
            await _storage.SetAccessTokenAsync(result.Token, cancellationToken);
            await _provider.LoginUser(result.Token);
            _logger.LogWarning("Successufully updated tokens");
        }
        else if (response.StatusCode is HttpStatusCode.BadRequest)
        {
            _logger.LogWarning("Exception Occured. Deleting tokens"); //TODO: remove
            await _storage.RemoveAccessTokenAsync(cancellationToken);
            await _storage.RemoveRefreshTokenAsync(cancellationToken);
            await _provider.LogoutUser();
            return false;
        }
        else
        {
            throw new InvalidOperationException("Something went wrong.");
        }
        
        return true;
    }
}
