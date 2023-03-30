using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Application.Abstractions;
public interface IJournal
{
    Task WriteAsync(JournalRecord records, CancellationToken cancellationToken);
}
