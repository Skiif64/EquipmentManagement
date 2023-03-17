using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EquipmentManagement.UI
{
    public class CustomAuthentificationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public CustomAuthentificationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {            
            var principal = new ClaimsPrincipal();
            var state = new AuthenticationState(principal);
            return Task.FromResult(state);
        }
    }
}
