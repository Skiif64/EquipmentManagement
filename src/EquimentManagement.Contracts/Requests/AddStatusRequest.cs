using System.ComponentModel.DataAnnotations;

namespace EquimentManagement.Contracts.Requests;

public class AddStatusRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
