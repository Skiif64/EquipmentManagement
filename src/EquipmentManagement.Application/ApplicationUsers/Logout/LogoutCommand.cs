using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.ApplicationUsers.Logout;
public class LogoutCommand : ICommand
{
    public Guid UserId { get; set; }

    public LogoutCommand(Guid id)
    {
        UserId = id;
    }
}
