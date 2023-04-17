using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Abstractions;

public interface IEquipmentTypeService
{
    Task<EquipmentTypeResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<EquipmentTypeResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Guid> AddAsync(AddEquipmentTypeRequest request, CancellationToken cancellationToken = default);
}
