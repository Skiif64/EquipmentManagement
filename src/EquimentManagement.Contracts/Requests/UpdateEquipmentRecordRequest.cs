using System.ComponentModel.DataAnnotations;

namespace EquimentManagement.Contracts.Requests;

public class UpdateEquipmentRecordRequest
{
    [Required]
    public Guid EquipmentRecordId { get; set; }
    public Guid? EmployeeId { get; set; }
    public Guid? StatusId { get; set; }
}
