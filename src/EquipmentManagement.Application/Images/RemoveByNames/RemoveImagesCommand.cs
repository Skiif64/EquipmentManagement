using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Images.RemoveByNames;
public class RemoveImagesCommand : ICommand
{
    public Guid EquipmentId { get; set; }
    public IEnumerable<string> ImageNames { get; set; }

    public RemoveImagesCommand(IEnumerable<string> imageNames)
    {
        ImageNames = imageNames;
    }
}
