using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Abstractions;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeResponse>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EmployeeResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(AddEmployeeRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken= default);
}
