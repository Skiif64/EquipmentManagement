using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentRecords.GetByEquipmentId;
public class GetEquipmentRecordByEquipmentIdQueryHandler
    : IQueryHandler<GetEquipmentRecordByEquipmentIdQuery, IEnumerable<EquipmentRecord>?>
{
    private readonly IApplicationDbContext _context;

    public GetEquipmentRecordByEquipmentIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<EquipmentRecord>?> Handle(GetEquipmentRecordByEquipmentIdQuery request, CancellationToken cancellationToken)
    {
        var records = _context
            .Set<EquipmentRecord>()
            .Where(x => x.EquipmentId == request.EquipmentId);

        return Task.FromResult(records?.AsEnumerable());
    }
}
