using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EventPlus.Domain.Context;

public interface ISqlServerDatabase
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancel = default);
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}