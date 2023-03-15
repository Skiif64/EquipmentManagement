using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class EquipmentStatus : BaseModel
{
	public Guid? EmployeeId { get; init; }
	public Guid EquipmentId { get; init; }
	public Guid StatusId { get; init; }
	public DateTimeOffset Modified { get; init; }
    public EquipmentStatus(Guid? employeeId,
                           Guid equipmentId,
                           Guid statusId,
                           DateTimeOffset modified)
    {
        EmployeeId = employeeId;
        EquipmentId = equipmentId;
        StatusId = statusId;
        Modified = modified;
    }

    protected EquipmentStatus() : base()
    {

    }
}
