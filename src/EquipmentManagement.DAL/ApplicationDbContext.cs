using EquipmentManagement.Application.Abstractions;
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
        Database.Migrate();        
    }

    DbSet<TEntity> IApplicationDbContext.Set<TEntity>()
        => Set<TEntity>();
    
}
