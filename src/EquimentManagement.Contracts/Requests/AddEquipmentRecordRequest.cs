using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EquimentManagement.Contracts.Requests;

public class AddEquipmentRecordRequest
{
    public Guid? EmployeeId { get; set; }
    [Required]
    public Guid EquipmentId { get; set; }
    [Required]
    [NotNull]
    public Guid? StatusId { get; set; }
}
