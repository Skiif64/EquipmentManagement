using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Employees.Get;

public class GetEmployeeQuery : IQuery<Employee?>
{
    public Guid Id { get; set; }
	public GetEmployeeQuery(Guid id)
	{
		Id = id;
	}
}
