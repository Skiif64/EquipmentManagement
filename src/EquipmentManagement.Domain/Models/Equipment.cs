using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Equipment : BaseModel
{
    private EquipmentRecord? _lastRecord;
    public EquipmentType Type { get; init; } = null!;
	public string Article { get; init; } = string.Empty;
    public string SerialNumber { get; init; } = string.Empty;
    public string? Description { get; init; }
    public EquipmentRecord? LastRecord => _lastRecord
        ??= GetLastRecord();
    public virtual IList<Image> Images { get; set; } = null!;
    public virtual IList<EquipmentRecord> Records { get; init; } = null!;

    protected Equipment() : base()
    {

    }

    private EquipmentRecord? GetLastRecord()
    {
        if(Records is null || !Records.Any())
            return null;

        return Records.MaxBy(x => x.Modified);
    }
}
