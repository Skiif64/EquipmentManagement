using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Auth;

internal class UserStoreDbContext : IdentityDbContext
{
	public UserStoreDbContext(DbContextOptions options) : base(options)
	{
		Database.Migrate();
	}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);        
    }
}
