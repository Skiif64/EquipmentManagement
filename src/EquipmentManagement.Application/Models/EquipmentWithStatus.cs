using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Models;

public class EquipmentWithStatus
{
    public Equipment Equipment { get; set; }
    public Status? Status { get; set; }    
}
