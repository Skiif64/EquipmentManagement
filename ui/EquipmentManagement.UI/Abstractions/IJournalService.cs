using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Abstractions;

public interface IJournalService
{
    Task<IEnumerable<JournalRecordResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<JournalRecordResponse>> GetAsync(int count = 20, int offset = 0, CancellationToken cancellationToken = default);
}
