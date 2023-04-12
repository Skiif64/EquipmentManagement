using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Models;
using System.Net.Http.Json;

namespace EquipmentManagement.UI.Services;

public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<ApplicationUserResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users = await _client.GetFromJsonAsync<IEnumerable<ApplicationUserResponse>>("/api/auth/", cancellationToken);
        return users ?? Enumerable.Empty<ApplicationUserResponse>();
    }

    public async Task<AuthentificationResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/auth/register/", request, cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<AuthentificationResult>();
        if (!result.IsSuccess)
            return new AuthentificationResult(result.Errors!);

        return new AuthentificationResult();
    }
}
