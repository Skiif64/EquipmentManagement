using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Status : BaseModel
{
	public string Title { get; }
	public string Description { get; }
    public Status(string title,
                  string description)
    {
        Title = title;
        Description = description;
    }
}
