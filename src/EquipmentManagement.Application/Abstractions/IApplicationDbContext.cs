using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Application.Abstractions;

public interface IApplicationDbContext
{
    IQueryable<TEntity> Set<TEntity>() where TEntity : BaseModel;

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
