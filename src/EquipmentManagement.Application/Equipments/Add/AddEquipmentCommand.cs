using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Equipments.Add;

public class AddEquipmentCommand : ICommand
{
    public string Type { get; set; }
    public string Article { get; set; }
    public string SerialNumber { get; set; }
    public string? Description { get; set; }    
}
