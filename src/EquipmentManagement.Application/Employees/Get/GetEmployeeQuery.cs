using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Employees.Get;

public class GetEmployeeQuery : IQuery<PagedList<Employee>>
{
    public int Page { get; }
    public int PageSize { get; }
    public string? SearchQuery { get; }

    public GetEmployeeQuery(int page, int pageSize, string? searchQuery = null)
    {
        Page = page;
        PageSize = pageSize;
        SearchQuery = searchQuery;
    }
}
