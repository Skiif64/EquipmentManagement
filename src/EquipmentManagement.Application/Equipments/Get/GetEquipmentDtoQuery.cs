﻿using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.Get;

public class GetEquipmentDtoQuery : IQuery<PagedList<EquipmentDto>>
{
    public int? Page { get; }
    public int? PageSize { get; }
    public string? Order { get; }
    public string? SearchCriteria { get; }

    public GetEquipmentDtoQuery(int? page, int? pageSize, string? order, string? searchCriteria)
    {
        Page = page;
        PageSize = pageSize;
        Order = order;
        SearchCriteria = searchCriteria;
    }
}