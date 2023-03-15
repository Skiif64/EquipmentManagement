using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Employees.Add;

public class AddEmployeeCommand : ICommand
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
}
