using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Utils;

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
        if (token is not null && TokenExpire(token))
            await RefreshTokenAsync(cancellationToken);
        var response = await base.SendAsync(request, cancellationToken);
        return response;
    }

    private bool TokenExpire(string token)
    {        
        var parsedToken = JwtTokenParser.Parse(token);
        if (parsedToken is null)
            return true;
        return parsedToken.ValidTo < DateTime.UtcNow;
    }

    private async Task RefreshTokenAsync(CancellationToken cancellationToken)
    {
        var refresher = _serviceProvider.GetRequiredService<IJwtTokenRefresher>();
        await refresher.RefreshAccessToken(cancellationToken);
    }
}
