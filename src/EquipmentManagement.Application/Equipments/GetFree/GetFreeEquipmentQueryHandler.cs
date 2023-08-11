using EquipmentManagement.Application.Abstractions;
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
            .Set<EquipmentRecord>()
            .Include(x => x.Equipment)
            .ThenInclude(x => x.Type)
            .Include(x => x.Equipment)
            .ThenInclude(x => x.Images)
            .Where(x => x.EmployeeId == null)
            .Select(x => x.Equipment);

        return Task.FromResult(equipments.AsEnumerable());
    }
}
