using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EquipmentManagement.UI.Authentification
{
    public class CustomAuthentificationStateProvider : AuthenticationStateProvider
    {
        private readonly UserCredentialStorage _storage;

        public CustomAuthentificationStateProvider(UserCredentialStorage storage)
        {
            _storage = storage;
        }

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
