using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Domain.Abstractions.Repositories;

public interface IEquipmentRepository : ICrudRepository<Equipment>
{
    IAsyncEnumerable<Equipment> GetAssigneedByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken);
}
