using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Equipment : BaseModel
{    
    public Guid TypeId { get; set; }
    public EquipmentType Type { get; set; } = null!;
	public string Article { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Author { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }    
    public virtual IList<Image> Images { get; set; } = null!;    

    protected Equipment() : base()
    {

    }    
}
