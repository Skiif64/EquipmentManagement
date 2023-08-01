using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Models;

namespace EquipmentManagement.UI.Abstractions;

public interface IEquipmentService
{
    Task<IEnumerable<EquipmentViewModel>?> GetAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<IEnumerable<EquipmentViewModel>?> GetByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<EquipmentViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<EquipmentViewModel>> GetFreeAsync(CancellationToken cancellationToken = default);
    Task AddAsync(AddEquipmentRequest request, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateEquipmentRequest request, CancellationToken cancellationToken = default);
    
}
