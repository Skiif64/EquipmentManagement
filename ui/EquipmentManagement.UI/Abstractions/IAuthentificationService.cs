using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Abstractions;

public interface IAuthentificationService
{
    Task<AuthentificationResult> SignInAsync(LoginRequest request, CancellationToken cancellationToken);
    Task<AuthentificationResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
    Task SignOutAsync(CancellationToken cancellationToken);
}
