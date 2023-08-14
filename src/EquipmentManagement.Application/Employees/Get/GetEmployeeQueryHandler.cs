using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Employees.Get;

public class GetEmployeeQueryHandler : IQueryHandler<GetEmployeeQuery, PagedList<Employee>>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeeQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Employee>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var set = _context
            .Set<Employee>()
            .OrderBy(x => x.Status)
            .ThenBy(x => x.Lastname)
            .ThenBy(x => x.Firstname)
            .AsEnumerable()
            .Where(x => EmployeeSelector(x, request.SearchQuery))
            .AsQueryable()
            ;

        return PagedList<Employee>.Create(set, request.Page, request.PageSize);        
    }

    private static bool EmployeeSelector(Employee employee, string? searchQuery)
    {
        var query = searchQuery ?? string.Empty;
        const StringComparison comparsion = StringComparison.InvariantCultureIgnoreCase;
        if (employee.Lastname.Contains(query, comparsion))
            return true;
        if (employee.Firstname.Contains(query, comparsion))
            return true;
        if (employee.Patronymic is not null
            && employee.Patronymic.Contains(query, comparsion))
            return true;

        return false;
    }
}
