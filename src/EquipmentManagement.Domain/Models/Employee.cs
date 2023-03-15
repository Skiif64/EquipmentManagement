using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Employee : BaseModel
{
    public string Firstname { get; init; } = string.Empty;
	public string Lastname { get; init; } = string.Empty;
    public string Patronymic { get; init; } = string.Empty;
    public Employee(string firstname,
                    string lastname,
                    string patronymic)
    {
        Firstname = firstname;
        Lastname = lastname;
        Patronymic = patronymic;
    }

    protected Employee() : base()
    {

    }
}
