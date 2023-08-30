using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Employees.Fire;

public class FireEmployeeCommandHandler : ICommandHandler<FireEmployeeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public FireEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(FireEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context
            .Set<Employee>()
            .Include(x => x.Records)
            .SingleOrDefaultAsync(x => x.Id == request.EmployeeId, cancellationToken);
        if (employee is null)
            throw new NotFoundException("Employee");
        employee.Status = EmployeeStatus.Fired;
        _context.Set<Employee>().Update(employee);
        await _context.SaveChangesAsync(cancellationToken);
        return employee.Id;
    }
}
