using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using EquipmentManagement.DAL.EntityConfiguration;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.DAL;

internal class ApplicationDbContext : DbContext, IApplicationDbContext, IMigrationableDatabase
{
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Equipment> Equipments { get; set; } = null!;
    public DbSet<EquipmentRecord> EquipmentRecords { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<JournalRecord> Journal { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public void Migrate()
        => Database.Migrate();


    public async Task MigrateAsync(CancellationToken cancellationToken)
        => await Database.MigrateAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new EquipmentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeTypeConfiguration());
    }    
    
    DbSet<TEntity> IApplicationDbContext.Set<TEntity>()
        => Set<TEntity>();

    
}
