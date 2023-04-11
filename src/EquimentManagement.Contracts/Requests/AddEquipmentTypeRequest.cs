namespace EquimentManagement.Contracts.Requests;
public class AddEquipmentTypeRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
