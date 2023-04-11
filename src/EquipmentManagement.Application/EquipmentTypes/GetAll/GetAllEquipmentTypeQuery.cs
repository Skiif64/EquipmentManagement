using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentTypes.GetAll;
public class GetAllEquipmentTypeQuery : IQuery<IEnumerable<EquipmentType>>
{
}
