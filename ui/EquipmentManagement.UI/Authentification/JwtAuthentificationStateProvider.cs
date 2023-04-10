using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Utils;
using Microsoft.AspNetCore.Components.Authorization;
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

    public void Notify()
        => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = await _tokenStorage.GetAccessTokenAsync();
        var identity = new ClaimsIdentity();
        if (!string.IsNullOrWhiteSpace(token))
        {
            var parsedToken = JwtTokenParser.Parse(token);
            if (parsedToken is not null)
            {
                identity = new ClaimsIdentity(parsedToken.Claims, "jwt",
                    JwtRegisteredClaimNames.Name,
                    ClaimTypes.Role);
            }

        }

        var state = new AuthenticationState(new ClaimsPrincipal(identity));

        return state;
    }
}
