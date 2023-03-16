using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class EquipmentRecord : BaseModel
{
	public Guid? EmployeeId { get; set; }
	public Guid EquipmentId { get; set; }
	public Guid StatusId { get; set; }
	public DateTimeOffset Modified { get; set; }    

    protected EquipmentRecord() : base()
    {

    }
}
