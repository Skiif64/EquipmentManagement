using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;
public class Image : BaseModel
{
    public Guid EquipmentId { get; set; }
    public Equipment Equipment { get; set; } = null!;
    public string FullImagePath { get; set; } = string.Empty;
    public string ThumbImagePath { get; set; } = string.Empty;
    public Image() : base()
    {
        
    }
}
