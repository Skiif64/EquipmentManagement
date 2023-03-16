using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.DAL;

internal class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Equipment> Equipments { get; set; } = null!;
    public DbSet<EquipmentStatus> EquipmentStatuses { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {        
        Database.EnsureCreated();
    }

    IQueryable<TEntity> IApplicationDbContext.Set<TEntity>()
        => Set<TEntity>();
    
}
