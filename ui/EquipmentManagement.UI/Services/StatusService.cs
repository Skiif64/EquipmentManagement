using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using System.Net.Http.Json;

namespace EquipmentManagement.UI.Services;

public class StatusService : IStatusService
{
    private readonly HttpClient _client;

    public StatusService(HttpClient client)
    {
        _client = client;
    }

    public async Task AddAsync(AddStatusRequest request, CancellationToken cancellationToken = default)
    {
        await _client.PostAsJsonAsync("/api/status/add/", request, cancellationToken);
    }

    public async Task<IEnumerable<StatusResponse>?> GetAll(CancellationToken cancellationToken = default)
    {
        var statuses = await _client.GetFromJsonAsync<IEnumerable<StatusResponse>?>("/api/status/");
        return statuses;
    }

    public async Task<StatusResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var status = await _client.GetFromJsonAsync<StatusResponse?>($"api/status/{id}");
    }
}
