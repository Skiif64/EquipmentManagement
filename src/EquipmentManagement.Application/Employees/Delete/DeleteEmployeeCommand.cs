using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Employees.Delete;
public class DeleteEmployeeCommand : ICommand
{
    public Guid Id { get; }

    public DeleteEmployeeCommand(Guid id)
    {
        Id = id;
    }
}
