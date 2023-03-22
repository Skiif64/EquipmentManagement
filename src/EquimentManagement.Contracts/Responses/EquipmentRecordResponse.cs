namespace EquimentManagement.Contracts.Responses;

public class EquipmentRecordResponse
{
    public Guid Id { get; set; }
    public virtual Guid? EmployeeId { get; set; }
    public virtual Guid EquipmentId { get; set; }
    public virtual Guid StatusId { get; set; }
    public DateTimeOffset Modified { get; set; }
}
