using EventPlus.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EventPlus.Domain.Context;

public partial class SqlServerDbContext
{
    #region Remove

    public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
    {
        if (entity is not ISoftDeletable softDeletableEntity) return base.Remove(entity);

        softDeletableEntity.Deleted = DateTime.UtcNow;

        return base.Entry(entity);
    }

    public override EntityEntry Remove(object entity)
    {
        if (entity is not ISoftDeletable softDeletableEntity) return base.Remove(entity);

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
            softDeletableEntity.Deleted = DateTime.UtcNow;
        }

        base.RemoveRange(deletableEntities);
    }

    #endregion
}