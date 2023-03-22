using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Abstractions;

public interface IEquipmentService
{
    Task<IEnumerable<EquipmentResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EquipmentResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
