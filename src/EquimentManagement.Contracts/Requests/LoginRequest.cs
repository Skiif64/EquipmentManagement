using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EquimentManagement.Contracts.Requests;

public class LoginRequest
{
    [Required]
    public string Login { get; set; } = string.Empty;
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}
