using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;
using System.Diagnostics.CodeAnalysis;

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
        var groupedRecords = _context
            .Set<EquipmentRecord>()
            .Where(r => r.EmployeeId == request.EmployeeId)
            .GroupBy(r => r.EquipmentId)            
            ;
        var records = groupedRecords
            .Select(r => r.MaxBy(r => r.Modified))            
            ;

        return Task.FromResult(records?.AsEnumerable());
    }
}
