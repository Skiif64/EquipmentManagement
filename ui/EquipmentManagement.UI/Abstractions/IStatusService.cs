using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Abstractions;

public interface IStatusService
{
    Task AddAsync(AddStatusRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<StatusResponse>?> GetAll(CancellationToken cancellationToken = default);
    Task<StatusResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
