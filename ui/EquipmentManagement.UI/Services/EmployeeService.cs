using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace EquipmentManagement.UI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _client;

    public EmployeeService(HttpClient client)
    {
        _client = client;
    }

    public async Task AddAsync(AddEmployeeRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _client.PostAsJsonAsync("/api/employees/add/", request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var request = new FireEmployeeRequest
        {
            EmployeeId = id
        };
        var content = JsonContent.Create(request);
        var response = await _client.PatchAsync($"/api/employees/delete", content, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<EmployeeResponse>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var employees = await _client.GetFromJsonAsync<IEnumerable<EmployeeResponse>>("/api/employees", cancellationToken);
        return employees;
    }

    public async Task<EmployeeResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var employee = await _client.GetFromJsonAsync<EmployeeResponse>($"/api/employees/{id}", cancellationToken);
        return employee;
    }

    public async Task RestoreAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var request = new RestoreEmployeeRequest
        {
            EmployeeId = id
        };
        var content = JsonContent.Create(request);
        var response = await _client.PatchAsync("/api/employees/restore", content, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(UpdateEmployeeRequest request, CancellationToken cancellationToken = default)
    {
        var content = JsonContent.Create(request);
        var response = await _client.PatchAsync("/api/employees/update", content, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
