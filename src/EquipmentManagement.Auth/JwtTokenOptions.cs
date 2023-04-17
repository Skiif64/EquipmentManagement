namespace EquipmentManagement.Auth;
public class JwtTokenOptions
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public int TokenLifetimeMinutes { get; init; } = 5;
}
