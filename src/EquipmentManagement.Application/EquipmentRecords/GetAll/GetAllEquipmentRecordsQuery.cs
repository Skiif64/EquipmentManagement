using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentRecords.GetAll;

public class GetAllEquipmentRecordsQuery : IQuery<IEnumerable<EquipmentRecord>>
{
}
