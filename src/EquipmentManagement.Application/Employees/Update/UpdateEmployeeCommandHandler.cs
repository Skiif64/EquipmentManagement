using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Employees.Update;
internal class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context
            .Set<Employee>()
            .FindAsync(new object[] { request.EmployeeId }, cancellationToken)
            ?? throw new NotFoundException("Employee");

        employee.Firstname = request.Firstname;
        employee.Lastname = request.Lastname;
        employee.Patronymic = request.Patronymic;
        _context
            .Set<Employee>()
            .Update(employee);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
