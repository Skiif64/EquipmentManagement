using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.AddEmployee;

public class AddEmployeeCommand : ICommand
{
    public string Firstname { get; }
    public string Lastname { get; }
    public string Patronymic { get; }
    public AddEmployeeCommand(string firstname,
                              string lastname,
                              string patronymic)
    {
        Firstname = firstname;
        Lastname = lastname;
        Patronymic = patronymic;
    }
}
