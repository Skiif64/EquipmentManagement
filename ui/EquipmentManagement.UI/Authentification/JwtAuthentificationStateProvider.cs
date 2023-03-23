using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EquipmentManagement.UI.Authentification
{
    public class JwtAuthentificationStateProvider : AuthenticationStateProvider
    {
        
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "User"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var claimsIdentity = new ClaimsIdentity(claims, "test");
            var state = new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
            return Task.FromResult(state);
        }
    }
}
