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
            ;

        return await PagedList<Employee>.CreateAsync(set, request.Page, request.PageSize);        
    }
}
