using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
        var equipments = _context
            .Set<Equipment>()
            .Include(x => x.Records)
            .Include(x => x.Type)
            .AsEnumerable()
            .Where(x =>x.LastRecord != null && x.LastRecord.EmployeeId == request.EmployeeId)            
            ;
               
        return Task.FromResult(equipments?.AsEnumerable());
    }
}
