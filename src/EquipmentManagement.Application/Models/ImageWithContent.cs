using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Models;
public class ImageWithContent : Image
{
    public string? ContentType { get; init; }
    public Stream? Content { get; init; }
}
