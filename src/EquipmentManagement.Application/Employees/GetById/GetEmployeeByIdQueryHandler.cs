using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Employees.GetById;

public class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, Employee?>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeeByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _context
            .Set<Employee>()
            .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        return employee;
    }
}
