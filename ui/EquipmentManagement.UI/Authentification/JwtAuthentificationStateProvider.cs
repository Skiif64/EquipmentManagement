﻿using EquipmentManagement.UI.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EquipmentManagement.UI.Authentification;

public class JwtAuthentificationStateProvider : AuthenticationStateProvider, IAuthenticationStateNotifier
{
    private readonly ITokenStorage _tokenStorage;

    public JwtAuthentificationStateProvider(ITokenStorage tokenStorage)
    {
        _tokenStorage = tokenStorage;
    }

    public void StateChanged()
        => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = await _tokenStorage.GetAccessTokenAsync();
        var identity = new ClaimsIdentity();
        if (!string.IsNullOrWhiteSpace(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var parsedToken = handler.ReadJwtToken(token);
            var expirationDate = DateTimeOffset.FromUnixTimeSeconds((long)parsedToken.Payload.Exp!);
            if (DateTimeOffset.UtcNow <= expirationDate)
                identity = new ClaimsIdentity(parsedToken.Claims, "jwt");

        }

        var state = new AuthenticationState(new ClaimsPrincipal(identity));

        return state;
    }
}
