using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Application.ApplicationUsers.GetAll;
internal class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IEnumerable<ApplicationUser>>
{
    private readonly IApplicationDbContext _context;

    public GetAllUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<ApplicationUser>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _context
            .Set<ApplicationUser>();

        return Task.FromResult(users.AsEnumerable());
    }
}
