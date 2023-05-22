using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Utils;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;

namespace EquipmentManagement.UI.Authentification;

public class AuthenticationHttpMessageHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokenStorage;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AuthenticationHttpMessageHandler> _logger;
    private readonly NavigationManager _navigationManager;
    private readonly IAuthenticationStateNotifier _notifier;

    public AuthenticationHttpMessageHandler(ITokenStorage tokenStorage,
                                            IServiceProvider serviceProvider,
                                            ILogger<AuthenticationHttpMessageHandler> logger,
                                            NavigationManager navigationManager,
                                            IAuthenticationStateNotifier notifier)
    {
        _tokenStorage = tokenStorage;
        _serviceProvider = serviceProvider;
        _logger = logger;
        _navigationManager = navigationManager;
        _notifier = notifier;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await TryRefreshTokenAsync(cancellationToken);

        await AddBearerTokenAsync(request, cancellationToken);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is HttpStatusCode.Unauthorized)
            await RedirectToLoginAsync(cancellationToken);

        return response;
    }

    private async Task<bool> TryRefreshTokenAsync(CancellationToken cancellationToken)
    {
        var token = await _tokenStorage.GetAccessTokenAsync(cancellationToken);
        if (IsTokenExpired(token))
        {
            var refresher = _serviceProvider.GetRequiredService<IJwtTokenRefresher>();            
            if (!await refresher.RefreshAccessToken(cancellationToken))
            {
                _logger.LogWarning("Cannot refresh Tokens.");
                return false;
            }
        }
        return true;
    }

    private async Task AddBearerTokenAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenStorage.GetAccessTokenAsync(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private async Task RedirectToLoginAsync(CancellationToken cancellationToken)
    {
        _navigationManager.NavigateTo(PagePaths.Auth.Login);
        await _tokenStorage.RemoveRefreshTokenAsync(cancellationToken);
        await _tokenStorage.RemoveAccessTokenAsync(cancellationToken);
        _notifier.Notify();
    }

    private bool IsTokenExpired(string? token)
    {
        if (token is null)
            return true;
        var parsedToken = JwtTokenParser.Parse(token);
        if (parsedToken is null)
            return true;
        _logger.LogTrace("Token valid to: {valid}", parsedToken.ValidTo.ToLocalTime());
        return parsedToken.ValidTo < DateTime.UtcNow;
    }    
}
