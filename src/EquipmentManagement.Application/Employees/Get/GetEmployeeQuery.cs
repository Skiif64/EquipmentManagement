using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Employees.Get;

public class GetEmployeeQuery : IQuery<PagedList<Employee>>
{
    public int Page { get; }
    public int PageSize { get; }

    public GetEmployeeQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}
