using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Statuses.GetAll;

public class GetAllStatusQueryHandler : IQueryHandler<GetAllStatusQuery, IEnumerable<Status>>
{
    private IApplicationDbContext _context;

    public GetAllStatusQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Status>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
    {
        var statuses = _context.Set<Status>();
        return Task.FromResult(statuses.AsEnumerable());
    }
}
