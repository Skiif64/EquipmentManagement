using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using EquipmentManagement.Auth.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Auth;

public class UsersDbContext : DbContext, IUserDbContext, IMigrationableDatabase
{
	public DbSet<ApplicationUser> Users { get; set; } = null!;
    public UsersDbContext(DbContextOptions options) : base(options)
	{
		
	}

    public void Migrate()
        => Database.Migrate();

    public async Task MigrateAsync(CancellationToken cancellationToken)
        => await Database.MigrateAsync(cancellationToken);

    DbSet<TEntity> IApplicationDbContext.Set<TEntity>()
       => Set<TEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {       
        modelBuilder.ApplyConfiguration(new ApplicationUserEntityTypeConfiguration());
    }
}
