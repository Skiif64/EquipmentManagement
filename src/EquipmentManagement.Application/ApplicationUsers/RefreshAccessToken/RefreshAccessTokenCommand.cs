using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Application.ApplicationUsers.RefreshAccessToken;
public class RefreshAccessTokenCommand : ICommand<AuthenticationResult>
{
    public Guid RefreshToken { get; set; }
    public RefreshAccessTokenCommand(Guid refreshToken)
    {
        RefreshToken = refreshToken;
    }
}

