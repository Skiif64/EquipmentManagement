using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
            .Include(x => x.Employee)
            .Include(x => x.Status)
            .Where(x => x.EquipmentId == request.EquipmentId);

        return Task.FromResult(records?.AsEnumerable());
    }
}
