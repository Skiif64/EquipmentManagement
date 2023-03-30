namespace EquimentManagement.Contracts.Responses;
public class ApplicationUserResponse
{
    public string Login { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
}
