using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Employees.Delete;
internal sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context
            .Set<Employee>()
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException("Employee");

        var records = _context
            .Set<EquipmentRecord>()
            .Where(x => x.EmployeeId == request.Id);

        _context
            .Set<EquipmentRecord>()
            .RemoveRange(records);

        _context
            .Set<Employee>()
            .Remove(employee);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
