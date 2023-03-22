using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Application.Equipments.GetAllWithStatus;

public class GetAllEquipmentWithStatusQuery : IQuery<IEnumerable<EquipmentWithStatus>?>
{
}
