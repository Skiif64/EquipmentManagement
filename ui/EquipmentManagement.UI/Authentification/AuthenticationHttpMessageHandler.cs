using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Utils;
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
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await base.SendAsync(request, cancellationToken);
        
        return response;
    }

    
}
