using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Employees.Update;
public class UpdateEmployeeCommand : ICommand
{
    public Guid EmployeeId { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string? Patronymic { get; set; }

}
