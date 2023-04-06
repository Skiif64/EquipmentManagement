namespace EquimentManagement.Contracts.Responses;

public class EquipmentResponse
{
    public Guid Id { get; set; }
    public string Type { get; init; } = string.Empty;
    public string Article { get; init; } = string.Empty;
    public string SerialNumber { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Guid? LastRecordId { get; set; }
    public IEnumerable<string> ImageNames { get; set; } = Enumerable.Empty<string>();
}
