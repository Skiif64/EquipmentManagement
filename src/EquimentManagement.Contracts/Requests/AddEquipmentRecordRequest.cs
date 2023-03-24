using System.ComponentModel.DataAnnotations;

namespace EquimentManagement.Contracts.Requests;

public class AddEquipmentRecordRequest
{
    public Guid? EmployeeId { get; set; }
    [Required]
    public Guid EquipmentId { get; set; }
    [Required]    
    public Guid StatusId { get; set; }
}
