using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Employees.AddEmployee;

public class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = _mapper.Map<Employee>(request);
        await _context
            .Set<Employee>()
            .AddAsync(employee, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return employee.Id;
    }
}
