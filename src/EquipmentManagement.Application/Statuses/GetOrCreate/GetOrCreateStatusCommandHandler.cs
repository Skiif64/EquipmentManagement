using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Statuses.GetOrCreate;
internal class GetOrCreateStatusCommandHandler : ICommandHandler<GetOrCreateStatusCommand, Status>
{
    private readonly IApplicationDbContext _context;

    public GetOrCreateStatusCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Status> Handle(GetOrCreateStatusCommand request, CancellationToken cancellationToken)
    {
        Status? status = await _context.Set<Status>()
            .FirstOrDefaultAsync(x => x.Title == request.Title, cancellationToken);
        if( status is null)
        {
            status = new Status
            {
                Title = request.Title,
                Records = new List<EquipmentRecord>()
            };
            await _context.Set<Status>().AddAsync(status, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return status;
        }
        
        return status;
    }
}
