using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Status : BaseModel
{
	public string Title { get; init; } = string.Empty;
	public string Description { get; init; } = string.Empty;
    protected Status() : base()
    {

    }
}
