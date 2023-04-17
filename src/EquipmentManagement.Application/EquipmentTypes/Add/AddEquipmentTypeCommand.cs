using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.EquipmentTypes.Add;
public class AddEquipmentTypeCommand : ICommand<Guid>
{
    public string Name { get; set; } = string.Empty;
}
