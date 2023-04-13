using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Authentification;
using EquipmentManagement.UI.Utils;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;

namespace EquipmentManagement.UI.Services;

public class HttpService
{
    private readonly HttpClient _client;
    private readonly ITokenStorage _storage;
    private readonly NavigationManager _navigationManager;
    private readonly IAuthenticationStateNotifier _notifier;

    public HttpService(HttpClient client, ITokenStorage tokenStorage, NavigationManager navigationManager, IAuthenticationStateNotifier notifier)
    {
        _client = client;
        _storage = tokenStorage;
        _navigationManager = navigationManager;
        _notifier = notifier;
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await _client.SendAsync(request, cancellationToken);
    }

    public async Task<HttpResponseMessage> AuthorizedSendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _storage.GetAccessTokenAsync(cancellationToken);

        if (!IsTokenValid(token))
        {
            token = await RefreshTokenAsync(cancellationToken);
            if (token is null)
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await SendAsync(request, cancellationToken);
        if (response.StatusCode is HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo(PagePaths.Auth.Login);
        }
        return response;

    }

    private static bool IsTokenValid(string? token)
    {
        if (token is null)
            return false;
        var parsedToken = JwtTokenParser.Parse(token);
        if (parsedToken is null)
            return false;

        return parsedToken.ValidTo < DateTime.UtcNow;
    }

    private async Task<string?> RefreshTokenAsync(CancellationToken cancellationToken)
    {
        if (!await TryRefreshTokenAsync(cancellationToken))
        {
            _navigationManager.NavigateTo(PagePaths.Auth.Login, true);
            await _storage.RemoveRefreshTokenAsync(cancellationToken);
            await _storage.RemoveAccessTokenAsync(cancellationToken);
        }
        return await _storage.GetAccessTokenAsync(cancellationToken);
    }

    private async Task<bool> TryRefreshTokenAsync(CancellationToken cancellationToken)
    {
        var refreshToken = await _storage.GetRefreshTokenAsync(cancellationToken);
        if (refreshToken is null)
            return false;

        var response = await _client.GetFromJsonAsync<AuthentificationResponse>($"/api/auth/refresh/{refreshToken}", cancellationToken);
        if (response is null || !response.IsSuccess)
            return false;
        await _storage.SetRefreshTokenAsync(response.RefreshToken!, cancellationToken);
        await _storage.SetAccessTokenAsync(response.Token, cancellationToken);

        _notifier.Notify();
        return true;
    }
}
