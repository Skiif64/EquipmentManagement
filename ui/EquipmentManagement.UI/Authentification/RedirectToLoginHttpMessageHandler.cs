using EquipmentManagement.UI.Utils;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace EquipmentManagement.UI.Authentification;

public class RedirectToLoginHttpMessageHandler : DelegatingHandler
{
    private readonly NavigationManager _navigationManager;

    public RedirectToLoginHttpMessageHandler(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
            _navigationManager.NavigateTo(PagePaths.Auth.Login);
        return response;        
    }
}
