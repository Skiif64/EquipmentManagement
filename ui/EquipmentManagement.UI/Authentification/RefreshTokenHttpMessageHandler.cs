using EquipmentManagement.UI.Utils;

namespace EquipmentManagement.UI.Authentification;

public class RefreshTokenHttpMessageHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = base.SendAsync(request, cancellationToken);
        return response;
    }

    private bool TokenExpire(string? token)
    {
        if (token is null)
            return true;
        var parsedToken = JwtTokenParser.Parse(token);
        return parsedToken.ValidTo > DateTime.UtcNow;
    }
}
