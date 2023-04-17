using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Application.Models;
public class JournalRecord : BaseModel
{
    public string? Username { get; set; }   
    public string Message { get; set; } = string.Empty;
    public string EventName { get; set; } = string.Empty;
    public DateTimeOffset? DateCreated { get; set; }
    public JournalRecord() : base()
    {
        
    }
}
