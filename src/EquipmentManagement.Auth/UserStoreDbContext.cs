using EquipmentManagement.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Auth;

public class UserStoreDbContext : DbContext
{
	public DbSet<ApplicationUser> Users { get; set; } = null!;
    public UserStoreDbContext(DbContextOptions options) : base(options)
	{
		Database.Migrate();
	}    
}
