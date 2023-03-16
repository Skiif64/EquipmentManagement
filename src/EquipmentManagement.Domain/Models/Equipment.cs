using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Equipment : BaseModel
{
	public string Type { get; init; } = string.Empty;
	public string Article { get; init; } = string.Empty;
    public string SerialNumber { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    protected Equipment() : base()
    {

    }
}
