﻿using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Abstractions;

public interface IEmployeeService
{
    Task<PagedListResponse<EmployeeResponse>> GetAsync(int page, int pageSize, string? query = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<EmployeeResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EmployeeResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(AddEmployeeRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken= default);
    Task FireAsync(Guid id, CancellationToken cancellationToken= default);
    Task RestoreAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateEmployeeRequest request, CancellationToken cancellationToken = default);
}
