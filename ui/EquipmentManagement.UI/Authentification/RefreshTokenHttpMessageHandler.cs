using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Utils;
using System.Net;

namespace EquipmentManagement.UI.Authentification;

public class RefreshTokenHttpMessageHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokenStorage;
    private readonly IServiceProvider _serviceProvider;
    public RefreshTokenHttpMessageHandler(ITokenStorage tokenStorage, IServiceProvider serviceProvider)
    {
        _tokenStorage = tokenStorage;
        _serviceProvider = serviceProvider;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenStorage.GetAccessTokenAsync(cancellationToken);        
        if (IsTokenExpired(token))
        {
            if (!await RefreshTokenAsync(cancellationToken))
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
        var response = await base.SendAsync(request, cancellationToken);
        return response;
    }

    private static bool IsTokenExpired(string? token)
    {
        if (token is null)
            return true;
        var parsedToken = JwtTokenParser.Parse(token);
        if (parsedToken is null)
            return true;
        return parsedToken.ValidTo < DateTime.UtcNow;
    }

    private async Task<bool> RefreshTokenAsync(CancellationToken cancellationToken)
    {
        var refresher = _serviceProvider.GetRequiredService<IJwtTokenRefresher>();
        return await refresher.RefreshAccessToken(cancellationToken);
    }
}
