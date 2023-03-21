using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Statuses.GetAll;

public class GetAllStatusQuery : IQuery<IEnumerable<Status>?>
{
}
