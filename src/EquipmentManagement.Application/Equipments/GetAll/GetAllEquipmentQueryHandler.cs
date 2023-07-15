using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.GetAll;

public class GetAllEquipmentQueryHandler : IQueryHandler<GetAllEquipmentQuery, IEnumerable<Equipment>>
{
    private readonly IApplicationDbContext _context;

    public GetAllEquipmentQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Equipment>> Handle(GetAllEquipmentQuery request, CancellationToken cancellationToken)
    {
        var set = _context.Set<Equipment>();
        return Task.FromResult(set.AsEnumerable());
    }
}
