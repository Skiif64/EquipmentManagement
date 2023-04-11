using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentTypes.GetById;
public class GetEquipmentTypeByIdQuery : IQuery<EquipmentType?>
{
    public Guid TypeId { get; set; }

    public GetEquipmentTypeByIdQuery(Guid typeId)
    {
        TypeId = typeId;
    }
}
