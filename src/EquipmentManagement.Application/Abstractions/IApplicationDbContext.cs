using EquipmentManagement.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : BaseModel;

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
