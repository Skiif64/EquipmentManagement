namespace EquipmentManagement.UI.Authentification;

public interface IJwtTokenRefresher
{
    Task RefreshAccessToken(string refreshToken, CancellationToken cancellationToken);
}
