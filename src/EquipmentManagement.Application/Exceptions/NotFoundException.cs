namespace EquipmentManagement.Application.Exceptions;

public class NotFoundException : Exception
{
	public NotFoundException(string entityName, Guid entityId) 
		: base($"{entityName} with id {entityId} not found.")
	{

	}
}
