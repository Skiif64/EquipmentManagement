using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Utils;
using System.Net;

namespace EquipmentManagement.UI.Authentification;

public class RefreshTokenHttpMessageHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokenStorage;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RefreshTokenHttpMessageHandler> _logger;
    public RefreshTokenHttpMessageHandler(ITokenStorage tokenStorage, IServiceProvider serviceProvider, ILogger<RefreshTokenHttpMessageHandler> logger)
    {
        _tokenStorage = tokenStorage;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenStorage.GetAccessTokenAsync(cancellationToken);        
        if (IsTokenExpired(token))
        {
            if (!await RefreshTokenAsync(cancellationToken))
            {
                _logger.LogWarning("Cannot refresh Tokens.");
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
        var response = await base.SendAsync(request, cancellationToken);
        return response;
    }

    private  bool IsTokenExpired(string? token)
    {
        if (token is null)
            return true;
        var parsedToken = JwtTokenParser.Parse(token);
        if (parsedToken is null)
            return true;
        _logger.LogTrace("Token valid to: {valid}", parsedToken.ValidTo.ToLocalTime());
        return parsedToken.ValidTo < DateTime.UtcNow;
    }

    private async Task<bool> RefreshTokenAsync(CancellationToken cancellationToken)
    {
        var refresher = _serviceProvider.GetRequiredService<IJwtTokenRefresher>();
        return await refresher.RefreshAccessToken(cancellationToken);
    }
}
