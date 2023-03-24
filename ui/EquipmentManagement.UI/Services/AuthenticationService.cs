using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Models;
using System.Net.Http.Json;

namespace EquipmentManagement.UI.Services;

public class AuthenticationService : IAuthentificationService
{
    private readonly HttpClient _client;
    private readonly ITokenStorage _storage;
    private readonly IAuthenticationStateNotifier _notifier;

    public AuthenticationService(HttpClient client, ITokenStorage storage, IAuthenticationStateNotifier notifier)
    {
        _client = client;
        _storage = storage;
        _notifier = notifier;
    }
    public async Task<AuthentificationResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/auth/register/", request, cancellationToken);
        //TODO: validate
        return new AuthentificationResult();
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
        _notifier.Notify();
        return new AuthentificationResult();
    }

    public async Task SignOutAsync(CancellationToken cancellationToken)
    {
        await _storage.RemoveAccessTokenAsync(cancellationToken);
        _notifier.Notify();
        var response = await _client.GetAsync("/api/auth/logout");
        if (!response.IsSuccessStatusCode)
            return;
    }
}
