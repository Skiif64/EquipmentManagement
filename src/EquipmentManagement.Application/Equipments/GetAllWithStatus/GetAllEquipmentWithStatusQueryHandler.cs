using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.GetAllWithStatus;

public class GetAllEquipmentWithStatusQueryHandler
    : IQueryHandler<GetAllEquipmentWithStatusQuery, IEnumerable<EquipmentWithStatus>?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllEquipmentWithStatusQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<IEnumerable<EquipmentWithStatus>?> Handle(GetAllEquipmentWithStatusQuery request, CancellationToken cancellationToken)
    {
        var entities = _context
            .Set<Equipment>()
            .Include(x => x.Records)            
            .ThenInclude(x => x.Status)
            .Include(x => x.Records)
            .ThenInclude(x => x.Employee)
            .Include(x => x.Images);
        var statuses = _mapper.Map<IEnumerable<EquipmentWithStatus>>(entities);
        return Task.FromResult(statuses);
    }
}
