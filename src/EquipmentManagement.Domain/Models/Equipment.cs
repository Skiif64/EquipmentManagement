using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Equipment : BaseModel
{
	public string Type { get; init; }
	public string Article { get; init; }
	public long SerialNumber { get; init; }
	public string Description { get; init; }
    public Equipment(string type,
                     string article,
                     long serialNumber,
                     string description)
    {
        Type = type;
        Article = article;
        SerialNumber = serialNumber;
        Description = description;
    }

    protected Equipment() : base()
    {

    }
}
