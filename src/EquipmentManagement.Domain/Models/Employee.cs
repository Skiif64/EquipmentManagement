using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Employee : BaseModel
{
	public string Firstname { get; }
	public string Lastname { get; }
	public string Patronymic { get; }
    public Employee(string firstname,
                    string lastname,
                    string patronymic)
    {
        Firstname = firstname;
        Lastname = lastname;
        Patronymic = patronymic;
    }
}
