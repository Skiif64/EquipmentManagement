using System.ComponentModel.DataAnnotations;

namespace EquimentManagement.Contracts.Requests;

internal class AddStatusRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
