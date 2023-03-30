using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Application.ApplicationUsers.SignIn;
public class SignInCommand : ICommand<AuthenticationResult>
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
