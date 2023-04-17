using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Equipments.Add;

public class AddEquipmentCommand : ICommand<Guid>
{
    public Guid TypeId { get; init; }
    public string Article { get; init; } = string.Empty;
    public string SerialNumber { get; init; } = string.Empty;
    public string? Description { get; set; }
    public string Author { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public IEnumerable<string>? ImageNames { get; set; }

    public AddEquipmentCommand()
    {
        CreatedAt = DateTimeOffset.UtcNow;
    }
}
