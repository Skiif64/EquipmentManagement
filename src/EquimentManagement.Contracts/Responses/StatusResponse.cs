namespace EquimentManagement.Contracts.Responses;

public class StatusResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
