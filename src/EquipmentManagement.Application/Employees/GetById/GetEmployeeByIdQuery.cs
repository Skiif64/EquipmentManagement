using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Employees.GetById;

public class GetEmployeeByIdQuery : IQuery<Employee?>
{
    public Guid Id { get; set; }
    public GetEmployeeByIdQuery(Guid id)
    {
        Id = id;
    }
}
