using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Models;

namespace EquipmentManagement.UI.Abstractions;

public interface IEquipmentService
{
    Task<PagedListResponse<EquipmentResponse>?> GetAsync(int page = 1, int pageSize = 10, string? criteria = null, string? filter = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<EquipmentResponse>?> GetByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<EquipmentResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<EquipmentResponse>> GetFreeAsync(CancellationToken cancellationToken = default);
    Task AddAsync(AddEquipmentRequest request, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateEquipmentRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
}
