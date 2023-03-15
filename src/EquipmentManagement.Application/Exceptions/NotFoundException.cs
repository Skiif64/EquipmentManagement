namespace EquipmentManagement.Application.Exceptions;

public class NotFoundException : Exception
{
	public NotFoundException(string name) : base($"entity {name} not found.")
	{

	}
}
