using System.ComponentModel.DataAnnotations;
namespace EquipmentManagement.WebApi.Requests;

public class AddEmployeeRequest
{
    [Required]
    public string Firstname { get; init; } = string.Empty;
    [Required]
    public string Lastname { get; init; } = string.Empty;
    public string? Patronymic { get; init; }
}
