using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Equipments.Update;
public class UpdateEquipmentCommand : ICommand<Guid>
{
    public Guid EquipmentId { get; set; }
    public Guid TypeId { get; init; }
    public string Article { get; init; } = string.Empty;
    public string SerialNumber { get; init; } = string.Empty;
    public string? Description { get; set; }
    public string Author { get; set; } = string.Empty;
    public DateTimeOffset LastModified { get; set; }
    public IEnumerable<string> NewImages { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<string> ImageToRemove { get; set; } = Enumerable.Empty<string>();

    public UpdateEquipmentCommand()
    {
        LastModified = DateTimeOffset.UtcNow;
    }
}
