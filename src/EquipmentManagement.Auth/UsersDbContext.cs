using EquipmentManagement.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Auth;

public class UsersDbContext : DbContext
{
	public DbSet<ApplicationUser> Users { get; set; } = null!;
    public UsersDbContext(DbContextOptions options) : base(options)
	{
		
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>()
            .HasData(new ApplicationUser
            {
                Login = "Admin",
                Password = "example",
                Role = "Admin"
            });
    }
}
