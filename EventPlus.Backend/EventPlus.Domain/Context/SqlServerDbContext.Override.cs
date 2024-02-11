using EventPlus.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EventPlus.Domain.Context;

public partial class SqlServerDbContext
{
    #region Remove

    public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
    {
        if (entity is not ISoftDeletable softDeletableEntity) return base.Remove(entity);

        if (softDeletableEntity.Deleted is null)
            softDeletableEntity.Deleted = DateTime.UtcNow;

        return base.Entry(entity);
    }

    public override EntityEntry Remove(object entity)
    {
        if (entity is not ISoftDeletable softDeletableEntity) return base.Remove(entity);

        if (softDeletableEntity.Deleted is null)
            softDeletableEntity.Deleted = DateTime.UtcNow;

        return base.Entry(entity);
    }

    #endregion

    #region RemoveRange

    public override void RemoveRange(params object[] entities)
    {
        var softDeletableEntities = entities
            .Where(e => e is ISoftDeletable)
            .Cast<ISoftDeletable>()
            .ToArray();
        var deletableEntities = entities.Except(softDeletableEntities);

        foreach (var softDeletableEntity in softDeletableEntities)
        {
            if (softDeletableEntity.Deleted is null)
                softDeletableEntity.Deleted = DateTime.UtcNow;
        }

        base.RemoveRange(deletableEntities);
    }

    public override void RemoveRange(IEnumerable<object> entities)
    {
        var enumerated = entities.ToArray();

        var softDeletableEntities = enumerated
            .Where(e => e is ISoftDeletable)
            .Cast<ISoftDeletable>()
            .ToArray();
        var deletableEntities = enumerated.Except(softDeletableEntities);

        foreach (var softDeletableEntity in softDeletableEntities)
        {
            if (softDeletableEntity.Deleted is null)
                softDeletableEntity.Deleted = DateTime.UtcNow;
        }

        base.RemoveRange(deletableEntities);
    }

    #endregion

    #region SaveChanges

    public override async Task<int> SaveChangesAsync(CancellationToken cancel = default)
    {
        ChangeTracker.DetectChanges();

        var modified = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Modified)
            .Where(t => t.Entity is IUpdatable)
            .ToArray();

        foreach (var entry in modified)
        {
            if (entry.Entity is ISoftDeletable)
            {
                var deletedPropertyName = nameof(ISoftDeletable.Deleted);

                var originalValue = entry.Property(deletedPropertyName).OriginalValue;
                var currentValue = entry.Property(deletedPropertyName).CurrentValue;

                if (originalValue is null && currentValue is not null)
                    continue;
            }


            ((IUpdatable)entry.Entity).Updated = DateTime.UtcNow;
            ((IUpdatable)entry.Entity).UpdatedBy = UserId;
        }

        return await base.SaveChangesAsync(cancel);
    }

    #endregion
}