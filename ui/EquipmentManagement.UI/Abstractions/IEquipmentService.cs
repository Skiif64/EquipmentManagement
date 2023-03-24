using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Abstractions;

public interface IEquipmentService
{
    Task<IEnumerable<EquipmentWithStatusResponse>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<EquipmentResponse>?> GetByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<EquipmentResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(AddEquipmentRequest request, CancellationToken cancellationToken = default);
    
    
}
