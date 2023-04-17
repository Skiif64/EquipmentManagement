namespace EquimentManagement.Contracts.Responses;
public class AuthentificationResponse
{
    public bool IsSuccess { get; init; }
    public string Token { get; init; } = string.Empty;
    public string? RefreshToken { get; set; }
    public IDictionary<string, string>? Errors { get; init; }
}
