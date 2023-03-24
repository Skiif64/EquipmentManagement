namespace EquimentManagement.Contracts.Responses;

public class EquipmentWithStatusResponse : EquipmentResponse
{
    public Guid? EmployeeId { get; set; }
    public string? EmployeeFullname { get; set; }
    public Guid? StatusId { get; set; }
    public string? StatusTitle { get; set; }    
    public EquipmentWithStatusResponse() : base()
    {
        
    }
}
