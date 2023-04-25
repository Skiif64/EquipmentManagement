using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Models;

public class Equipment : BaseModel
{
    private EquipmentRecord? _lastRecord;
    public Guid TypeId { get; set; }
    public EquipmentType Type { get; set; } = null!;
	public string Article { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Author { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public EquipmentRecord? LastRecord => _lastRecord
        ??= GetLastRecord();
    public virtual IList<Image> Images { get; set; } = null!;
    public virtual IList<EquipmentRecord> Records { get; set; } = null!;

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
