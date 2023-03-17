using System.ComponentModel.DataAnnotations;

namespace EquimentManagement.Contracts.Requests;

public class UpdateEquipmentRecordRequest
{
    [Required]
    public Guid EquipmentRecordId { get; init; }
    public Guid? EmployeeId { get; init; }
    public Guid? StatusId { get; init; }
}
