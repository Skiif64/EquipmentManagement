﻿using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Application.Models;
public class JournalRecord : BaseModel
{
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string Message { get; set; } = string.Empty;
    public string EventName { get; set; } = string.Empty;
    protected JournalRecord() : base()
    {
        
    }
}
