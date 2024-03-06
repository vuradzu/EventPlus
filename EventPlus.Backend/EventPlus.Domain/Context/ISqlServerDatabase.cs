using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EventPlus.Domain.Context;

public interface ISqlServerDatabase
{
    long? UserId { get; set; }
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
    void RemoveRange(IEnumerable<object> entities);
    Task<int> SaveChangesAsync(CancellationToken cancel = default);
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}