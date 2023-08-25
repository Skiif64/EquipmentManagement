using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using EquipmentManagement.DAL.EntityConfiguration;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EquipmentManagement.Integration.Tests")]

namespace EquipmentManagement.DAL;

internal class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Equipment> Equipments { get; set; } = null!;
    public DbSet<EquipmentRecord> EquipmentRecords { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<JournalRecord> Journal { get; set; } = null!;
    public DbSet<EquipmentType> EquipmentTypes { get; set; } = null!;
    public DbSet<ApplicationUser> Users { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new EquipmentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationUserEntityTypeConfiguration());
    }    
    
    DbSet<TEntity> IApplicationDbContext.Set<TEntity>()
        => Set<TEntity>();

    
}
