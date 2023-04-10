using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class EquipmentRecord : BaseModel
{
    public Guid? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
    public Guid EquipmentId { get; set; }
    public virtual Equipment Equipment { get; set; } = null!;
    public Guid StatusId { get; set; }
    public virtual Status Status { get; set; } = null!;
	public DateTimeOffset Modified { get; set; }
    public string ModifyAuthor { get; set; } = string.Empty;

    protected EquipmentRecord() : base()
    {

    }
}
