using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Employees.Delete;

public class DeleteEmployeeCommand : ICommand<Guid>
{
    public Guid EmployeeId { get; set; }
    public DeleteEmployeeCommand(Guid id)
    {
        EmployeeId = id;
    }
}
