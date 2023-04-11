using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentTypes.GetById;
internal class GetEquipmentTypeByIdQueryHandler : IQueryHandler<GetEquipmentTypeByIdQuery, EquipmentType?>
{
    private readonly IApplicationDbContext _context;

    public GetEquipmentTypeByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EquipmentType?> Handle(GetEquipmentTypeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Set<EquipmentType>()
            .FindAsync(new[] { request.TypeId }, cancellationToken);
    }
}
