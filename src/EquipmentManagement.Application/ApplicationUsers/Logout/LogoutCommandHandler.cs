using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.ApplicationUsers.Logout;
internal class LogoutCommandHandler : ICommandHandler<LogoutCommand>
{
    private readonly IApplicationDbContext _context;

    public LogoutCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Set<ApplicationUser>()
            .FindAsync(new object[] { request.UserId }, cancellationToken);
        if (user is null)
            return;
        user.RefreshToken = null;
        _context
        .Set<ApplicationUser>()
            .Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
