using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Abstractions;

public interface IEquipmentRecordService
{
    Task<IEnumerable<EquipmentRecordResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(AddEquipmentRecordRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<EquipmentRecordResponse>> GetByEquipmentIdAsync(Guid equipmentId, CancellationToken cancellationToken = default);
}
