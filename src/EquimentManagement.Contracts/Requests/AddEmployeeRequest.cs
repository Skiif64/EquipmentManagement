using System.ComponentModel.DataAnnotations;
namespace EquimentManagement.Contracts.Requests;

public class AddEmployeeRequest
{
    [Required]
    public string Firstname { get; set; } = string.Empty;
    [Required]
    public string Lastname { get; set; } = string.Empty;
    public string? Patronymic { get; set; }
}
