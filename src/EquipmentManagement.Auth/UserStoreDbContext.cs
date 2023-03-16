using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Auth;

internal class UserStoreDbContext : IdentityDbContext
{
	public UserStoreDbContext(DbContextOptions options) : base(options)
	{
		Database.Migrate();
	}
}
