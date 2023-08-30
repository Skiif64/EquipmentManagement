using EquipmentManagement.Domain.Models;
using MediatR;

namespace EquipmentManagement.Application.Equipments.Create;
public class EquipmentCreatedEvent : INotification
{
    public Equipment Equipment { get; }

    public EquipmentCreatedEvent(Equipment equipment)
    {
        Equipment = equipment;
    }
}
