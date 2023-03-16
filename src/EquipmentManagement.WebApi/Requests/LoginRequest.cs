using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagement.WebApi.Requests;

public class LoginRequest
{
    [Required]
    public string Login { get; init; } = string.Empty;
    [Required]
    [PasswordPropertyText]
    public string Password { get; init; } = string.Empty;
    public bool RememberMe { get; init; }
}
