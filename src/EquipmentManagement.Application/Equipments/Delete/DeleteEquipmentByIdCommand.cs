using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application.Equipments.Delete;
public class DeleteEquipmentByIdCommand : ICommand
{
    public Guid Id { get; }
    public DeleteEquipmentByIdCommand(Guid id)
    {
        Id = id;
    }
}
