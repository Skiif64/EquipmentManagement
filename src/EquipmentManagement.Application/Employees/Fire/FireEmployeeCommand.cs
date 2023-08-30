using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Employees.Fire;

public class FireEmployeeCommand : ICommand<Guid>
{
    public Guid EmployeeId { get; set; }
    public FireEmployeeCommand(Guid id)
    {
        EmployeeId = id;
    }
}
