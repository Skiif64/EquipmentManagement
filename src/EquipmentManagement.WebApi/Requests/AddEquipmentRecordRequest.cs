namespace EquipmentManagement.WebApi.Requests;

public class AddEquipmentRecordRequest
{
    public Guid? EmployeeId { get; init; }
    public Guid EquipmentId { get; init; }
    public Guid StatusId { get; init; }
}
