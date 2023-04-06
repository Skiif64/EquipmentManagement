using System.ComponentModel.DataAnnotations;

namespace EquimentManagement.Contracts.Requests;


public class AddEquipmentRequest
{
    [Required]
    public string Type { get; set; } = string.Empty;
    [Required]
    public string Article { get; set; } = string.Empty;
    [Required]
    public string SerialNumber { get; set; } = string.Empty;
    public string? Description { get; set; }
    public IEnumerable<string>? ImageNames { get; set; }
}
