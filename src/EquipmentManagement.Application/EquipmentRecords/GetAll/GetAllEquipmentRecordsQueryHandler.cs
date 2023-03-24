using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentRecords.GetAll;

public class GetAllEquipmentRecordsQueryHandler : IQueryHandler<GetAllEquipmentRecordsQuery, IEnumerable<EquipmentRecord>>
{
    private readonly IApplicationDbContext _context;

    public GetAllEquipmentRecordsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<EquipmentRecord>> Handle(GetAllEquipmentRecordsQuery request, CancellationToken cancellationToken)
    {
        var records = _context
            .Set<EquipmentRecord>();
        return Task.FromResult(records.AsEnumerable());
    }
}
