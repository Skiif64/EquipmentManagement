using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Statuses.Get;

public class GetStatusByIdQueryHandler : IQueryHandler<GetStatusByIdQuery, Status>
{
    private readonly IApplicationDbContext _context;

    public GetStatusByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Status> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Set<Status>()
            .SingleOrDefaultAsync(x => x.Id == request.StatusId, cancellationToken)
            ?? throw new NotFoundException("Status");
    }
}
