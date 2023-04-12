using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.UI.Models;

namespace EquipmentManagement.UI.Abstractions;

public interface IUserService
{
    Task<IEnumerable<ApplicationUserResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AuthentificationResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
}
