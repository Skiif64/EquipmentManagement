using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Authentification;
using EquipmentManagement.UI.Models;
using System.Net;
using System.Net.Http.Json;

namespace EquipmentManagement.UI.Services;

public class AuthenticationService : IAuthentificationService
{
    private readonly HttpClient _client;
    private readonly ITokenStorage _storage;
    private readonly JwtAuthentificationStateProvider _provider;

    public AuthenticationService(IHttpClientFactory factory, ITokenStorage storage, JwtAuthentificationStateProvider provider)
    {
        _client = factory.CreateClient("Auth");
        _storage = storage;       
        _provider = provider;
    }

    public async Task<AuthentificationResult> SignInAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/auth/login", request, cancellationToken);

        var authResponse = await response.Content.ReadFromJsonAsync<AuthentificationResponse>();
        if (authResponse is null)
            throw new Exception();//TODO: normal exception
        if (!authResponse.IsSuccess)
            return new AuthentificationResult(authResponse.Errors!);
        await _storage.SetAccessTokenAsync(authResponse.Token, cancellationToken);
        await _storage.SetRefreshTokenAsync(authResponse.RefreshToken!, cancellationToken);
        await _provider.LoginUser(authResponse.Token);
        return new AuthentificationResult();
    }

    public async Task SignOutAsync(Guid userId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/api/auth/logout/{userId}");
        
        await _storage.RemoveAccessTokenAsync(cancellationToken);
        await _storage.RemoveRefreshTokenAsync(cancellationToken);
        await _provider.LogoutUser();
    }
}
