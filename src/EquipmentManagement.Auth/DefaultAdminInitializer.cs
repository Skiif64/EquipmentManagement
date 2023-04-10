using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EquipmentManagement.Auth;
internal class DefaultAdminInitializer : IMigrationableDatabase
{
    private const string AdminLogin = "AdminUsername";
    private const string AdminPassword = "AdminPassword";
    private readonly IConfiguration _configuration;
    private readonly IUserDbContext _context;

    public DefaultAdminInitializer(IConfiguration configuration, IUserDbContext userDbContext)
    {
        _configuration = configuration;
        _context = userDbContext;
    }

    public void Migrate()
    {
        var user = CreateAdminUser();
        if(!_context.Set<ApplicationUser>()
            .Any(x => x.Role == Roles.Admin))
        {
            _context.Set<ApplicationUser>()
                .Add(user);
            _context.SaveChanges();
        }
    }

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        var user = CreateAdminUser();
        if (!await _context.Set<ApplicationUser>()
            .AnyAsync(x => x.Role == Roles.Admin, cancellationToken))
        {
            await _context.Set<ApplicationUser>()
                .AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    private ApplicationUser CreateAdminUser()
    {
        var login = _configuration.GetSection(AdminLogin).Value ?? "Admin";
        var password = _configuration.GetSection(AdminPassword).Value ?? "example";

        return ApplicationUser.Create(login, password, Roles.Admin);
    }
}
