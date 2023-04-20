﻿using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;

namespace EquipmentManagement.UI.Authentification;

public class RedirectToLoginHttpMessageHandler : DelegatingHandler
{
    private readonly NavigationManager _navigationManager;
    private readonly JwtAuthentificationStateProvider _authenticationStateProvider;
    private readonly ITokenStorage _storage;

    public RedirectToLoginHttpMessageHandler(NavigationManager navigationManager, ITokenStorage storage, JwtAuthentificationStateProvider authenticationStateProvider)
    {
        _navigationManager = navigationManager;
        _storage = storage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        if (response.StatusCode is HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo(PagePaths.Auth.Login);
            await _storage.RemoveRefreshTokenAsync(cancellationToken);
            await _storage.RemoveAccessTokenAsync(cancellationToken);
            await _authenticationStateProvider.LogoutUser();
        }
        return response;        
    }
}
