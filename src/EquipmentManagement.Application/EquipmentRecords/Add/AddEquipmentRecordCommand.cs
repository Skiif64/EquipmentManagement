using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.EquipmentRecords.Add;

public class AddEquipmentRecordCommand : ICommand<Guid>
{
    public Guid? EmployeeId { get; init; }
    public Guid EquipmentId { get; set; }
    public Guid StatusId { get; init; }
    public DateTimeOffset Modified { get; }

    public AddEquipmentRecordCommand()
    {
        Modified = DateTimeOffset.Now;
    }
}
