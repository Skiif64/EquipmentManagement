using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using System.Net.Http.Json;

namespace EquipmentManagement.UI.Services;

public class EquipmentService : IEquipmentService
{
    
    private readonly HttpClient _client;

    public EquipmentService(HttpClient client)
    {        
        _client = client;
    }

    public async Task AddAsync(AddEquipmentRequest request, CancellationToken cancellationToken = default)
    {
        await _client.PostAsJsonAsync("/api/equipment/add/", request, cancellationToken);
    }

    public async Task<IEnumerable<EquipmentWithStatusResponse>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        
        var equipments = await _client.GetFromJsonAsync<IEnumerable<EquipmentWithStatusResponse>>("/api/equipment/", cancellationToken);
        
        return equipments;
    }

    public async Task<IEnumerable<EquipmentResponse>?> GetByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        var equipments = await _client.GetFromJsonAsync<IEnumerable<EquipmentResponse>>($"/api/equipment/employee/{employeeId}", cancellationToken);
        return equipments;
    }

    public async Task<EquipmentResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var equipment = await _client.GetFromJsonAsync<EquipmentResponse>($"/api/equipment/{id}", cancellationToken);
        return equipment;
    }

    public async Task<IEnumerable<EquipmentResponse>> GetFreeAsync(CancellationToken cancellationToken = default)
    {
        var equipments = await _client.GetFromJsonAsync<IEnumerable<EquipmentResponse>>("/api/equipment/free", cancellationToken);
        return equipments ?? Enumerable.Empty<EquipmentResponse>();
    }

    public async Task UpdateAsync(UpdateEquipmentRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _client.PostAsJsonAsync("/api/equipment/update", request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
