﻿using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Status : BaseModel
{
	public string Title { get; init; } = string.Empty;
	public string? Description { get; init; }
    public virtual IList<EquipmentRecord> Records { get; init; } = null!;
    public Status() : base()
    {

    }
}
