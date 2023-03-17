using System.ComponentModel.DataAnnotations;

namespace EquimentManagement.Contracts.Requests;


public class AddEquipmentRequest
{
    [Required]
    public string Type { get; init; } = string.Empty;
    [Required]
    public string Article { get; init; } = string.Empty;
    [Required]
    public string SerialNumber { get; init; } = string.Empty;
    public string? Description { get; init; }
}
