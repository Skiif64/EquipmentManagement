using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Employees.Delete;

public class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public DeleteEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context
            .Set<Employee>()
            .FindAsync(request.EmployeeId);
        if (employee is null)
            throw new NotFoundException("Employee");

        _context
            .Set<Employee>()
            .Remove(employee);
        await _context.SaveChangesAsync(cancellationToken);
        return employee.Id;
    }
}
