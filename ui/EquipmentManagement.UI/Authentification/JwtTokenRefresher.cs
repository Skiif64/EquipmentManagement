﻿using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using System.Net;
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
        var response = await client.GetAsync($"/api/auth/refresh/{refreshToken}", cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AuthentificationResponse>(
                cancellationToken: cancellationToken)
                ?? throw new Exception(); //TODO: normal exception
            await _storage.SetRefreshTokenAsync(result.RefreshToken!, cancellationToken);
            await _storage.SetAccessTokenAsync(result.Token, cancellationToken);
        }
        else if (response.StatusCode is HttpStatusCode.BadRequest)
        {
            _logger.LogWarning("Exception Occured. Deleting tokens"); //TODO: remove
            await _storage.RemoveAccessTokenAsync(cancellationToken);
            await _storage.RemoveRefreshTokenAsync(cancellationToken);
            return false;
        }
        else
        {
            throw new InvalidOperationException("Something went wrong.");
        }

        _notifier.Notify();
        return true;
    }
}
