using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;
public class EquipmentType : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public IList<Equipment> Equipments { get; set; } = null!;
}
