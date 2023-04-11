using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Abstractions;

public interface IUserService
{
    Task<IEnumerable<ApplicationUserResponse>> GetAllAsync(CancellationToken cancellationToken = default);
}
