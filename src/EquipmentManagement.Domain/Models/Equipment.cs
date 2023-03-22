using EquipmentManagement.Domain.Models.Base;
using System.Runtime.Serialization;

namespace EquipmentManagement.Domain.Models;

public class Equipment : BaseModel
{
    private EquipmentRecord? _lastRecord;
	public string Type { get; init; } = string.Empty;
	public string Article { get; init; } = string.Empty;
    public string SerialNumber { get; init; } = string.Empty;
    public string? Description { get; init; }
    public EquipmentRecord? LastRecord => _lastRecord
        ??= GetLastRecord();
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
