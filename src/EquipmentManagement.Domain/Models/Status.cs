using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Status : BaseModel
{
	public string Title { get; init; }
	public string Description { get; init; }
    public Status(string title,
                  string description)
    {
        Title = title;
        Description = description;
    }

    protected Status() : base()
    {

    }
}
