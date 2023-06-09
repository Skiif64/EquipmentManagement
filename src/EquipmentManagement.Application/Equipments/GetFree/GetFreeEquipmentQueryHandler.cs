﻿using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.GetFree;
internal class GetFreeEquipmentQueryHandler : IQueryHandler<GetFreeEquipmentQuery, IEnumerable<Equipment>>
{
    private const string DiscardedTitle = "Списано";
    private readonly IApplicationDbContext _context;

    public GetFreeEquipmentQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Equipment>> Handle(GetFreeEquipmentQuery request, CancellationToken cancellationToken)
    {
        var equipments = _context
            .Set<Equipment>()
            .Include(x => x.Type)
            .Include(x => x.Records)            
            .ThenInclude(x => x.Employee)
            .Include(x => x.Records)
            .ThenInclude(x => x.Status)
            .AsEnumerable()
            .Where(x => x.LastRecord != null)
            .Where(x => x.LastRecord!.Employee == null)
            .Where(x => x.LastRecord!.Status.Title != DiscardedTitle);

        return Task.FromResult(equipments);
    }
}
