using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Models;

namespace EquipmentManagement.UI.Abstractions;

public interface IAuthentificationService
{
    Task<AuthentificationResult> SignInAsync(LoginRequest request, CancellationToken cancellationToken);    
    Task SignOutAsync(Guid userId, CancellationToken cancellationToken);    
}
