using System.ComponentModel.DataAnnotations;

namespace EquimentManagement.Contracts.Requests;
public class UpdateEmployeeRequest
{
    [Required]
    public Guid EmployeeId { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string? Patronymic { get; set; }
}
