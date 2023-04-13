using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Abstractions;

public interface IStatusService
{
    Task<StatusResponse> AddAsync(AddStatusRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<StatusResponse>?> GetAll(CancellationToken cancellationToken = default);
    Task<StatusResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<StatusResponse?> GetOrCreateByNameAsync(string title, string? description = null, CancellationToken cancellationToken = default);
}
