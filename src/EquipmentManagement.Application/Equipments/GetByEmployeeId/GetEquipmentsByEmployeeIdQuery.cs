using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.GetByEmployeeId;

public class GetEquipmentsByEmployeeIdQuery : IQuery<IEnumerable<Equipment>>
{
    public Guid EmployeeId { get; set; }
    public GetEquipmentsByEmployeeIdQuery(Guid employeeId)
    {
        EmployeeId = employeeId;
    }
}
