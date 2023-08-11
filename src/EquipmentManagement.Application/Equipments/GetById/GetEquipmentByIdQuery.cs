using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.GetById;

public class GetEquipmentByIdQuery : IQuery<Equipment>
{
    public Guid EquipmentId { get; set; }
    public GetEquipmentByIdQuery(Guid id)
    {
        EquipmentId = id;
    }
}
