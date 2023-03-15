using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Domain.Abstractions.Repositories;

public interface IStatusRepository : ICrudRepository<Status>
{
    Task<Status> GetByEquipmentStatusIdAsync(Guid equipmentStatusId, CancellationToken cancellationToken);
}
