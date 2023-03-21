﻿using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Employee : BaseModel
{
    public string Firstname { get; init; } = string.Empty;
	public string Lastname { get; init; } = string.Empty;
    public string? Patronymic { get; init; }
    public virtual IList<Equipment> Equipments { get; init; }
    public virtual IList<EquipmentRecord> Records { get; init; }
    
    protected Employee() : base()
    {

    }
}
