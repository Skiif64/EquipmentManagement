using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Statuses.GetOrCreate;
public class GetOrCreateStatusCommand : ICommand<Status>
{
    public string Title { get; set; } = string.Empty;
}
