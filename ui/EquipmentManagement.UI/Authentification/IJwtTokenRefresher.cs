namespace EquipmentManagement.UI.Authentification;

public interface IJwtTokenRefresher
{
    Task RefreshAccessToken(CancellationToken cancellationToken);
}
