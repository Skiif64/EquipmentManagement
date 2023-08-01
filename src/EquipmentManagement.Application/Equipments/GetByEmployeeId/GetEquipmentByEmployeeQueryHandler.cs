using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.GetByEmployeeId;

public class GetEquipmentByEmployeeQueryHandler : IQueryHandler<GetEquipmentsByEmployeeIdQuery, IEnumerable<Equipment>>
{
    private readonly IApplicationDbContext _context;    

    public GetEquipmentByEmployeeQueryHandler(IApplicationDbContext context)                                              
    {
        _context = context;        
    }
    public Task<IEnumerable<Equipment>> Handle(GetEquipmentsByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var equipments = _context
            .Set<EquipmentRecord>()
            .Include(x => x.Equipment)
            .ThenInclude(x => x.Type)
            .Include(x => x.Equipment)
            .ThenInclude(x => x.Images)
            .Where(record => record.EmployeeId == request.EmployeeId)
            .Select(x => x.Equipment);
               
        return Task.FromResult(equipments.AsEnumerable());
    }
}
