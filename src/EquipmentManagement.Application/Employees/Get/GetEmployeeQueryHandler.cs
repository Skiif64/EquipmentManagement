using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Employees.Get;

public class GetEmployeeQueryHandler : IQueryHandler<GetEmployeeQuery, Employee?>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeeQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await _context
            .Set<Employee>()
            .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        return employee;
    }
}
