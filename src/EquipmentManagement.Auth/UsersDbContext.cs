using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Auth;

public class UsersDbContext : DbContext, IMigrationableDatabase
{
	public DbSet<ApplicationUser> Users { get; set; } = null!;
    public UsersDbContext(DbContextOptions options) : base(options)
	{
		
	}

    public void Migrate()
        => Database.Migrate();

    public async Task MigrateAsync(CancellationToken cancellationToken)
        => await Database.MigrateAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>()
            .HasData(ApplicationUser.Create("Admin", "example", "Admin"));
    }
}
