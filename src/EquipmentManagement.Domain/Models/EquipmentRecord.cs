using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class EquipmentRecord : BaseModel
{
	public virtual Employee? Employee { get; set; }
	public virtual Equipment Equipment { get; set; } = null!;
	public virtual Status Status { get; set; } = null!;
	public DateTimeOffset Modified { get; set; }    

    protected EquipmentRecord() : base()
    {

    }
}
