using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments;
public class EquipmentDto : Equipment
{
    public Status? CurrentStatus { get; init; }
    public Employee? CurrentEmployee { get; init; }
    public EquipmentDto() 
        : base()
    {
        
    }
}
