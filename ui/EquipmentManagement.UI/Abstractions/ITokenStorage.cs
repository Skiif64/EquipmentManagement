namespace EquipmentManagement.UI.Abstractions;

public interface ITokenStorage
{
    Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default);
    Task<string?> GetRefreshTokenAsync(CancellationToken cancellationToken = default);
    Task SetAccessTokenAsync(string token, CancellationToken cancellationToken = default);
    Task SetRefreshTokenAsync(string token, CancellationToken cancellationToken = default);
    Task RemoveAccessTokenAsync(CancellationToken cancellationToken);
    Task RemoveRefreshTokenAsync(CancellationToken cancellationToken);
}