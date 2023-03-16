using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.GetByEmployeeId;

public class GetEquipmentByEmployeeQueryHandler : IQueryHandler<GetEquipmentsByEmployeeIdQuery, IEnumerable<Equipment>?>
{
    private readonly IApplicationDbContext _context;    

    public GetEquipmentByEmployeeQueryHandler(IApplicationDbContext context)                                              
    {
        _context = context;        
    }
    public Task<IEnumerable<Equipment>?> Handle(GetEquipmentsByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var recordsIds = _context
            .Set<EquipmentStatus>()
            .Where(r => r.EmployeeId == request.EmployeeId)
            .Select(r => r.EquipmentId)
            ;
        var equipments = _context
            .Set<Equipment>()
            .Where(e => recordsIds.Contains(e.Id))
            ;
        return Task.FromResult(equipments?.AsEnumerable());
    }
}
