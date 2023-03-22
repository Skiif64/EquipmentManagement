using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Employee : BaseModel
{
    public string Firstname { get; init; } = string.Empty;
	public string Lastname { get; init; } = string.Empty;
    public string? Patronymic { get; init; }
    public string Fullname => $"{Lastname} {Firstname} {Patronymic}";
    public virtual IList<EquipmentRecord> Records { get; init; } = null!;
    
    protected Employee() : base()
    {

    }
}
