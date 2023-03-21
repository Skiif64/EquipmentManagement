using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Statuses.Add;

public class AddStatusCommand : ICommand<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
