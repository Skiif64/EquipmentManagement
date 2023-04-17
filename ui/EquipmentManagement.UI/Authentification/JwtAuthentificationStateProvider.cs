using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace EquipmentManagement.UI.Authentification;

public class JwtAuthentificationStateProvider : AuthenticationStateProvider
{   
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        var state = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));        
        return state;
    }

    public Task<AuthenticationState> LoginUser(string token)
    {
        var parsedToken = JwtTokenParser.Parse(token);
        if (parsedToken is null)
        {
            throw new ArgumentException("Invalid Token");
        }
            var identity = new ClaimsIdentity(parsedToken.Claims, "jwt",
                JwtRegisteredClaimNames.Name,
                ClaimTypes.Role);
            var state = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
            NotifyAuthenticationStateChanged(state);
            return state; 
    }

    public Task<AuthenticationState> LogoutUser()
    {
        var state = GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(state);
        return state;
    }
}
