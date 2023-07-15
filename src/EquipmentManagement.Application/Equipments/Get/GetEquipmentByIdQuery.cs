using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.Get;

public class GetEquipmentByIdQuery : IQuery<EquipmentWithStatus?>
{
    public Guid EquipmentId { get; set; }
    public GetEquipmentByIdQuery(Guid id)
    {
        EquipmentId = id;
    }
}
