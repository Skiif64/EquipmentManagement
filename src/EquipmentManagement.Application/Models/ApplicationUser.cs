using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Application.Models;

public class ApplicationUser : BaseModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
