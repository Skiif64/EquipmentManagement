using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Employees.GetAll;

public class GetAllEmployeeQuery : IQuery<IEnumerable<Employee>?>
{
}
