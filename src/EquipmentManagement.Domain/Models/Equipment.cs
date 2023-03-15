using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Equipment : BaseModel
{
	public string Type { get; }
	public string Article { get; }
	public long SerialNumber { get; }
	public string Description { get; }
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
}
