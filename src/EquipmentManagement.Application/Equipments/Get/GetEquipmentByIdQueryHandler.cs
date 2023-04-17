using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.Get;

public class GetEquipmentByIdQueryHandler : IQueryHandler<GetEquipmentByIdQuery, Equipment>
{
    private readonly IApplicationDbContext _context;

    public GetEquipmentByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Equipment> Handle(GetEquipmentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Set<Equipment>()
            .SingleOrDefaultAsync(x => x.Id == request.EquipmentId)            
            ?? throw new NotFoundException("Equipment");
    }
}
