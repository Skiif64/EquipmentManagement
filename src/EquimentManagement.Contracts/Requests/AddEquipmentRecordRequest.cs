using System.ComponentModel.DataAnnotations;

namespace EquimentManagement.Contracts.Requests;

public class AddEquipmentRecordRequest
{
    public Guid? EmployeeId { get; init; }
    [Required]
    public Guid EquipmentId { get; init; }
    [Required]
    public Guid StatusId { get; init; }
}
