using System.ComponentModel.DataAnnotations;

namespace EquimentManagement.Contracts.Requests;
public class UpdateEquipmentRequest
{
    [Required]
    public Guid EquipmentId { get; set; }
    [Required]
    public Guid TypeId { get; set; }
    [Required]
    public string Article { get; set; } = string.Empty;
    [Required]
    public string SerialNumber { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Author { get; set; } = string.Empty;
    public IEnumerable<string> ImageNames { get; set; } = Enumerable.Empty<string>();
}
