using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentRecords.GetByEquipmentId;
public class GetEquipmentRecordByEquipmentIdQuery : IQuery<IEnumerable<EquipmentRecord>?>
{
    public Guid EquipmentId { get; set; }
    public GetEquipmentRecordByEquipmentIdQuery(Guid equipmentId)
    {
        EquipmentId = equipmentId;
    }
}
