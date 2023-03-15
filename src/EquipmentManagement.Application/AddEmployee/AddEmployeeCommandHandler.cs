using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Abstractions.Repositories;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.AddEmployee;

public class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = _mapper.Map<Employee>(request);
        await _employeeRepository.CreateAsync(employee, cancellationToken);
    }
}
