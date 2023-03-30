namespace EquimentManagement.Contracts.Responses;
public class JournalRecordResponse
{
    public Guid? UserId { get; set; }
    public string? Username { get; set; }
    public string Message { get; set; } = string.Empty;
    public string EventName { get; set; } = string.Empty;
}
