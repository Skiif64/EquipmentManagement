namespace EquimentManagement.Contracts.Responses;

public class EquipmentWithStatusResponse : EquipmentResponse
{
    public Guid? AssignedTo { get; set; }
    public Guid? StatusId { get; set; }
    public string? StatusTitle { get; set; }
}
