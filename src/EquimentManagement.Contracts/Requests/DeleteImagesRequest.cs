namespace EquimentManagement.Contracts.Requests;
public class DeleteImagesRequest
{
    public Guid EquipmentID { get; set; }
    public IEnumerable<string> ImageNames { get; set; } = Enumerable.Empty<string>();
}
