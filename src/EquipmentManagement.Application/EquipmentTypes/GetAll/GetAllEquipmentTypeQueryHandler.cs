using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentTypes.GetAll;
internal class GetAllEquipmentTypeQueryHandler : IQueryHandler<GetAllEquipmentTypeQuery, IEnumerable<EquipmentType>>
{
    private readonly IApplicationDbContext _context;

    public GetAllEquipmentTypeQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<EquipmentType>> Handle(GetAllEquipmentTypeQuery request, CancellationToken cancellationToken)
    {
        var types = _context.Set<EquipmentType>();
        return Task.FromResult(types.AsEnumerable());
    }
}
