using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using System.Net.Http.Json;

namespace EquipmentManagement.UI.Services;

public class JournalService : IJournalService
{
    private readonly HttpClient _client;

    public JournalService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<JournalRecordResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await _client.GetFromJsonAsync<IEnumerable<JournalRecordResponse>>("/api/journal/all/", cancellationToken);
        return response;
    }

    public async Task<IEnumerable<JournalRecordResponse>> GetAsync(int count = 20, int offset = 0, CancellationToken cancellationToken = default)
    {
        var response = await _client.GetFromJsonAsync<IEnumerable<JournalRecordResponse>>($"/api/journal/{count}/{offset}", cancellationToken);
        return response;
    }
}
