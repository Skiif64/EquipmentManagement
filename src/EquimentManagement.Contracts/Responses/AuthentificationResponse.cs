namespace EquimentManagement.Contracts.Responses;
public class AuthentificationResult
{
    public bool IsSuccess { get; init; }
    public string Token { get; init; } = string.Empty;
    public IDictionary<string, string>? Errors { get; init; }
}
