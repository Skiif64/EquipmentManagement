using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Employee : BaseModel
{
    public string Firstname { get; set; } = string.Empty;
	public string Lastname { get; set; } = string.Empty;
    public string? Patronymic { get; set; }
    public string Fullname => $"{Lastname} {Firstname} {Patronymic}";
    public EmployeeStatus Status { get; set; }
    public virtual IList<EquipmentRecord> Records { get; set; } = null!;
    
    protected Employee() : base()
    {

    }
}
