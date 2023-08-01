using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.Get;

public class GetAllEquipmentWithStatusQuery : IQuery<PagedList<Equipment>>
{
    public int? Page { get; }
    public int? PageSize { get; }
    public string? Order { get; }

    public GetAllEquipmentWithStatusQuery(int? page, int? pageSize, string? order)
    {
        Page = page;
        PageSize = pageSize;
        Order = order;
    }
}
