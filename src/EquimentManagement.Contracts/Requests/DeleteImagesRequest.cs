namespace EquimentManagement.Contracts.Requests;
public class DeleteImagesRequest
{
    public Guid EquipmentId { get; set; }
    public IEnumerable<string> ImageNames { get; set; } = Enumerable.Empty<string>();
}
