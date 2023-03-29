namespace EquimentManagement.Contracts.Responses;

public class EquipmentRecordResponse
{
    public Guid Id { get; set; }
    public Guid? EmployeeId { get; set; }
    public string? EmployeeFullname { get; set; }
    public Guid EquipmentId { get; set; }
    public Guid StatusId { get; set; }
    public string StatusTitle { get; set; } = string.Empty;
    public DateTimeOffset Modified { get; set; }
}
