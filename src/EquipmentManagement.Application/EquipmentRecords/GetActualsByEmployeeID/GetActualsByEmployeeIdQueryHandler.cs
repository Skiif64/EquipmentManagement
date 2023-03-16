using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentRecords.GetActualsByEmployeeID;

public class GetActualsByEmployeeIdQueryHandler : IQueryHandler<GetActualsByEmployeeIdQuery, IEnumerable<EquipmentRecord>?>
{
    private readonly IApplicationDbContext _context;

    public GetActualsByEmployeeIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<EquipmentRecord>?> Handle(GetActualsByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var records = _context
            .Set<EquipmentRecord>()
            .Where(r => r.EmployeeId == request.EmployeeId)
            .OrderBy(r => r.Modified)
            .AsEnumerable()
            .DistinctBy(r => r.EquipmentId)            
            ;

        return Task.FromResult(records?.AsEnumerable());
    }
}
