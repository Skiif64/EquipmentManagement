using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.Get;

public class GetEquipmentDtoQuery : IQuery<PagedList<EquipmentDto>>
{
    public int? Page { get; }
    public int? PageSize { get; }
    public string? Order { get; }
    public string? SearchCriteria { get; }
    public string? Filter { get; }

    public GetEquipmentDtoQuery(int? page, int? pageSize, string? order, string? searchCriteria, string? filter)
    {
        Page = page;
        PageSize = pageSize;
        Order = order;
        SearchCriteria = searchCriteria;
        Filter = filter;
    }
}
