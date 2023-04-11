using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
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
}
