using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EquimentManagement.Contracts.Requests;

public class RegisterRequest
{
    [Required]
    public string Login { get; set; } = string.Empty;
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = string.Empty;
    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;
    [Required]
    public string Role { get; set; } = string.Empty;
}
