using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Models;

public class EquipmentWithStatus : Equipment
{
    public Guid? AssignedTo { get; set; }
    public Guid? StatusId { get; set; }
    public string? StatusTitle { get; set; }
    public EquipmentWithStatus() : base()
    {
        
    }
}
