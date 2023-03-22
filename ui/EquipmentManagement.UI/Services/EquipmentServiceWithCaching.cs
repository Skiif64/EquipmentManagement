using EquimentManagement.Contracts.Requests;
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

    public async Task AddAsync(AddEquipmentRequest request, CancellationToken cancellationToken = default)
    {
        await _client.PostAsJsonAsync("/api/equipment/add/", request, cancellationToken);
    }

    public async Task<IEnumerable<EquipmentResponse>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<EquipmentResponse>? equipments;
        if(_cache.TryGetValue(CachePrefix, out equipments ))
        {            
            return equipments;
        }
        equipments = await _client.GetFromJsonAsync<IEnumerable<EquipmentResponse>>("/api/equipment/", cancellationToken);
        _cache.Set(CachePrefix, equipments, new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(5)            
        });
        return equipments;
    }

    public async Task<EquipmentResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var equipment = await _client.GetFromJsonAsync<EquipmentResponse>($"/api/equipment/{id}", cancellationToken);
        return equipment;
    }
}
