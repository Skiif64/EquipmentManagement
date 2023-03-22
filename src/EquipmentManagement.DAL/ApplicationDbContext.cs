using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.DAL.EntityConfiguration;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.DAL;

internal class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Equipment> Equipments { get; set; } = null!;
    public DbSet<EquipmentRecord> EquipmentRecords { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new EquipmentTypeConfiguration());
    }
    DbSet<TEntity> IApplicationDbContext.Set<TEntity>()
        => Set<TEntity>();
    
}
