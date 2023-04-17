using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.GetAll;

public class GetAllEquipmentQuery : IQuery<IEnumerable<Equipment>?>
{
}
