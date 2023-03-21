using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Statuses.Get;

public class GetStatusByIdQuery : IQuery<Status>
{
    public Guid StatusId { get; set; }
    public GetStatusByIdQuery(Guid id)
    {
        StatusId = id;
    }
}
