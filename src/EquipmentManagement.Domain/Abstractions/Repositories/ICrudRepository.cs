using EquipmentManagement.Domain.Models.Base;

namespace EquipmentManagement.Domain.Abstractions.Repositories;

public interface ICrudRepository<TEntity> : IDisposable, IAsyncDisposable
    where TEntity : BaseModel
{    
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken);    
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);    
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);    
    IAsyncEnumerable<TEntity> GetAllAsync(CancellationToken cancellationToken);    
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
}
