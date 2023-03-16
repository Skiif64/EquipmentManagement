using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class EquipmentRecord : BaseModel
{
	public Guid? EmployeeId { get; init; }
	public Guid EquipmentId { get; init; }
	public Guid StatusId { get; init; }
	public DateTimeOffset Modified { get; init; }    

    protected EquipmentRecord() : base()
    {

    }
}
