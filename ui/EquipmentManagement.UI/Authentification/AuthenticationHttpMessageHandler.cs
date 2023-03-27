using EquipmentManagement.UI.Abstractions;
using System.Net;
using System.Net.Http.Headers;

namespace EquipmentManagement.UI.Authentification;

public class AuthenticationHttpMessageHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokenStorage;    

    public AuthenticationHttpMessageHandler(ITokenStorage tokenStorage)
    {
        _tokenStorage = tokenStorage;        
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenStorage.GetAccessTokenAsync(cancellationToken);
        if (TokenExpire(token))
            await RefreshTokenAsync(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await base.SendAsync(request, cancellationToken);
        
        return response;
    }    

    private bool TokenExpire(string? token)
    {
        if (token is null)
            return true;
        var parsedToken = JwtTokenParser.Parse(token);
        return parsedToken.ValidTo > DateTime.UtcNow;        
    }

    private async Task RefreshTokenAsync(CancellationToken cancellationToken)
    {
        
    }
}
