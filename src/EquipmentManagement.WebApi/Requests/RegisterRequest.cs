using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EquipmentManagement.WebApi.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string Login { get; init; } = string.Empty;
        [Required]
        [PasswordPropertyText]
        public string Password { get; init; } = string.Empty;
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; init; } = string.Empty;
    }
}
