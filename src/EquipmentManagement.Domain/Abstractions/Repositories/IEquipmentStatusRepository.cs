using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Domain.Abstractions.Repositories;

public interface IEquipmentStatusRepository : ICrudRepository<EquipmentStatus>
{
    Task<IEnumerable<EquipmentStatus>> GetStatusesByEquipmentIdAsync(Guid equipmentId, CancellationToken cancellationToken);
    Task<EquipmentStatus> GetActualEquipmentStatusAsync(Guid equipmentId, CancellationToken cancellationToken);
    Task<IEnumerable<EquipmentStatus>> GetActualByEmployeeId(Guid employeeId, CancellationToken cancellationToken);

}
