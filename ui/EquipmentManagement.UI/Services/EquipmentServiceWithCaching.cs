using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace EquipmentManagement.UI.Services;

public class EquipmentServiceWithCaching : IEquipmentService
{
    private const string CachePrefix = "Equipment";
    private readonly IMemoryCache _cache;
    private readonly HttpClient _client;

    public EquipmentServiceWithCaching(IMemoryCache cache, HttpClient client)
    {
        _cache = cache;
        _client = client;
    }

    public async Task<IEnumerable<EquipmentResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {        
        var equipments = await _client.GetFromJsonAsync<IEnumerable<EquipmentResponse>>("/api/equipment/");
        return equipments;
    }

    public Task<EquipmentResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
