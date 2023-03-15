using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class EquipmentStatus : BaseModel
{
	public Guid? EmployeeId { get; }
	public Guid EquipmentId { get; }
	public Guid StatusId { get; }
	public DateTimeOffset Modified { get; }
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
}
