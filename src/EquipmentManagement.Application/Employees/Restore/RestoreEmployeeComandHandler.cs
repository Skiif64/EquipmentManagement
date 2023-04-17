using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Employees.Restore;
internal class RestoreEmployeeComandHandler : ICommandHandler<RestoreEmployeeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public RestoreEmployeeComandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(RestoreEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context
            .Set<Employee>()
            .FindAsync(new object[] { request.EmployeeId }, cancellationToken);
        if (employee is null)
            throw new NotFoundException("Employee");

        employee.Status = EmployeeStatus.Active;
        _context
            .Set<Employee>()
            .Update(employee);
        await _context.SaveChangesAsync(cancellationToken);
        return employee.Id;
    }
}
