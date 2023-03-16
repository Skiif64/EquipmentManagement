using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class EquipmentRecord : BaseModel
{
	public Guid? EmployeeId { get; init; }
	public Guid EquipmentId { get; init; }
	public Guid StatusId { get; init; }
	public DateTimeOffset Modified { get; init; }
    public EquipmentRecord(Guid? employeeId,
                           Guid equipmentId,
                           Guid statusId,
                           DateTimeOffset modified)
    {
        EmployeeId = employeeId;
        EquipmentId = equipmentId;
        StatusId = statusId;
        Modified = modified;
    }

    protected EquipmentRecord() : base()
    {

    }
}
