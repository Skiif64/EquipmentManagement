using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using System.Net.Http.Json;

namespace EquipmentManagement.UI.Services;

public class EquipmentRecordService : IEquipmentRecordService
{
    private readonly HttpClient _client;

    public EquipmentRecordService(HttpClient client)
    {
        _client = client;
    }

    public async Task AddAsync(AddEquipmentRecordRequest request, CancellationToken cancellationToken = default)
    {
        await _client.PostAsJsonAsync("/api/equipmentrecords/add/", request, cancellationToken);
    }

    public async Task<IEnumerable<EquipmentRecordResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var records = await _client.GetFromJsonAsync<IEnumerable<EquipmentRecordResponse>>("/api/equipmentrecords/");
        return records;
    }
}
