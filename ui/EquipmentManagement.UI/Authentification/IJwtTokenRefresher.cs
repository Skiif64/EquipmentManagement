namespace EquipmentManagement.UI.Authentification;

public interface IJwtTokenRefresher
{
    Task<bool> RefreshAccessToken(CancellationToken cancellationToken);
}
