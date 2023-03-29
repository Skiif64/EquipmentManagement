using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Statuses.Add;

public class AddStatusCommand : ICommand<Status>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
