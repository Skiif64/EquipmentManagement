using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Employees.Restore;
public class RestoreEmployeeCommand : ICommand<Guid>
{
    public Guid EmployeeId { get; set; }

    public RestoreEmployeeCommand(Guid id)
    {
        EmployeeId = id;
    }
}
