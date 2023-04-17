using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using System.Net.Http.Json;

namespace EquipmentManagement.UI.Services;

public class EquipmentTypeService : IEquipmentTypeService
{
    private readonly HttpClient _client;

    public EquipmentTypeService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Guid> AddAsync(AddEquipmentTypeRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _client.PostAsJsonAsync("/api/type/", request, cancellationToken);
        return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<EquipmentTypeResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _client.GetFromJsonAsync<IEnumerable<EquipmentTypeResponse>>("/api/type/", cancellationToken)
            ?? Enumerable.Empty<EquipmentTypeResponse>();
    }

    public async Task<EquipmentTypeResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _client.GetFromJsonAsync<EquipmentTypeResponse>($"/api/type/{id}", cancellationToken);
    }
}
