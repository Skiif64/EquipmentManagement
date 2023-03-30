using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;
public class Image : BaseModel
{
    public Guid EquipmentId { get; init; }
    public Equipment Equipment { get; init; } = null!;
    public string FullImagePath { get; init; } = string.Empty;
    public string ThumbImagePath { get; init; } = string.Empty;
    protected Image() : base()
    {
        
    }
}
