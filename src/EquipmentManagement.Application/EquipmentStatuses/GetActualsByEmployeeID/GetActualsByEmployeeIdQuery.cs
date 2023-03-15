using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentStatuses.GetActualByEmployeeID;

public class GetActualsByEmployeeIdQuery : IQuery<IEnumerable<EquipmentStatus>>
{
    public Guid EmployeeId { get; set; }
    public GetActualsByEmployeeIdQuery(Guid employeeId)
    {
        EmployeeId = employeeId;
    }
}
