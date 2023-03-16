using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Equipments.Add;

public class AddEquipmentCommand : ICommand<Guid>
{
    public string Type { get; init; } = string.Empty;
    public string Article { get; init; } = string.Empty;
    public string SerialNumber { get; init; } = string.Empty;
    public string? Description { get; set; }    
}
