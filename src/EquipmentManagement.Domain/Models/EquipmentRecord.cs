using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class EquipmentRecord : BaseModel
{
	public Employee? Employee { get; set; }
	public Equipment Equipment { get; set; } = null!;
	public Status? Status { get; set; }
	public DateTimeOffset Modified { get; set; }    

    protected EquipmentRecord() : base()
    {

    }
}
