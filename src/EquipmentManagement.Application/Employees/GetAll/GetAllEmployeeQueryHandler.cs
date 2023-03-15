using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Abstractions.Repositories;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Employees.GetAll;

public class GetAllEmployeeQueryHandler : IQueryHandler<GetAllEmployeeQuery, IEnumerable<Employee>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeeQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<Employee>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        return await _employeeRepository.GetAllAsync(cancellationToken);
    }
}
