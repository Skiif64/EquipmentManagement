using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentRecords.Update;

public class UpdateEquipmentRecordCommand : ICommand<EquipmentRecord>
{
    public Guid EquipmentRecordId { get; init; }
    public Guid? EmployeeId { get; init; }
    public Guid? StatusId { get; init; }
    public DateTimeOffset Modified { get; init; }
    public UpdateEquipmentRecordCommand()
    {
        Modified= DateTimeOffset.Now;
    }
}
