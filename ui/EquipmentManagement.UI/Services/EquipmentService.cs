using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Models;
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

    public async Task<IEnumerable<EquipmentViewModel>?> GetAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        
        var equipments = await _client.GetFromJsonAsync<IEnumerable<EquipmentResponse>>(
            $"/api/equipment/?page={page}&pageSize={pageSize}", cancellationToken);        
        var models = equipments?.Select(equipment => new EquipmentViewModel
        {
            Id = equipment.Id,
            Article = equipment.Article,
            Author = equipment.Author,
            CreatedAt = equipment.CreatedAt,
            CurrentStatusTitle = null,
            Description = equipment.Description,
            EmployeeFullname = "",
            ImageNames = equipment.ImageNames,
            SerialNumber = equipment.SerialNumber,
            Type = equipment.Type,
            TypeId = equipment.TypeId,
        });
        return models;
    }

    public async Task<IEnumerable<EquipmentViewModel>?> GetByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        var equipments = await _client.GetFromJsonAsync<IEnumerable<EquipmentResponse>>(
            $"/api/equipment/employee/{employeeId}", cancellationToken);
        var models = equipments?.Select(equipment => new EquipmentViewModel
        {
            Id = equipment.Id,
            Article = equipment.Article,
            Author = equipment.Author,
            CreatedAt = equipment.CreatedAt,
            CurrentStatusTitle = null,
            Description = equipment.Description,
            EmployeeFullname = "",
            ImageNames = equipment.ImageNames,
            SerialNumber = equipment.SerialNumber,
            Type = equipment.Type,
            TypeId = equipment.TypeId,
        });
        return models;
    }

    public async Task<EquipmentViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var equipment = await _client.GetFromJsonAsync<EquipmentResponse>(
            $"/api/equipment/{id}", cancellationToken);
        if (equipment is null)
            return null;
        var model = new EquipmentViewModel
        {
            Id = equipment.Id,
            Article = equipment.Article,
            Author = equipment.Author,
            CreatedAt = equipment.CreatedAt,
            CurrentStatusTitle = null,
            Description = equipment.Description,
            EmployeeFullname = "",
            ImageNames = equipment.ImageNames,
            SerialNumber = equipment.SerialNumber,
            Type = equipment.Type,
            TypeId = equipment.TypeId,
        };
        return model;
    }

    public async Task<IEnumerable<EquipmentViewModel>> GetFreeAsync(CancellationToken cancellationToken = default)
    {
        var equipments = await _client.GetFromJsonAsync<IEnumerable<EquipmentResponse>>(
            "/api/equipment/free", cancellationToken);
        var models = equipments?.Select(equipment => new EquipmentViewModel
        {
            Id = equipment.Id,
            Article = equipment.Article,
            Author = equipment.Author,
            CreatedAt = equipment.CreatedAt,
            CurrentStatusTitle = null,
            Description = equipment.Description,
            EmployeeFullname = "",
            ImageNames = equipment.ImageNames,
            SerialNumber = equipment.SerialNumber,
            Type = equipment.Type,
            TypeId = equipment.TypeId,
        });
        return models ?? Enumerable.Empty<EquipmentViewModel>();
    }

    public async Task UpdateAsync(UpdateEquipmentRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _client.PostAsJsonAsync(
            "/api/equipment/update", request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
