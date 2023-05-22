using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Application.Models;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.Get;

public class GetEquipmentByIdQueryHandler : IQueryHandler<GetEquipmentByIdQuery, EquipmentWithStatus>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEquipmentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EquipmentWithStatus> Handle(GetEquipmentByIdQuery request, CancellationToken cancellationToken)
    {
        var equipment = await _context
            .Set<Equipment>()
            .Include(x => x.Type)
            .Include(x => x.Images)
            .SingleOrDefaultAsync(x => x.Id == request.EquipmentId)            
            ?? throw new NotFoundException("Equipment");
        return _mapper.Map<EquipmentWithStatus>(equipment);
    }
}
