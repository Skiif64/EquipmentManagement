using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentRecords.GetActualsByEmployeeID;

public class GetActualsByEmployeeIdQuery : IQuery<IEnumerable<EquipmentRecord>?>
{
    public Guid EmployeeId { get; set; }
    public GetActualsByEmployeeIdQuery(Guid employeeId)
    {
        EmployeeId = employeeId;
    }
}
