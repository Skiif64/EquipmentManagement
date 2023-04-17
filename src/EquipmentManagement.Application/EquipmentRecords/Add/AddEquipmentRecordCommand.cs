using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.EquipmentRecords.Add;

public class AddEquipmentRecordCommand : ICommand<Guid>
{
    public Guid? EmployeeId { get; init; }
    public Guid EquipmentId { get; init; }
    public Guid StatusId { get; init; }
    public DateTimeOffset Modified { get; }
    public string ModifyAuthor { get; init; } = string.Empty;

    public AddEquipmentRecordCommand()
    {
        Modified = DateTimeOffset.Now;
    }
}
