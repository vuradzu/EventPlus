using System.Linq.Expressions;
using EventPlus.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Domain.Extensions;

public static class ModelBuilderExtensions
{
    public static void UseSoftDeletableEntities(this ModelBuilder modelBuilder)
    {
        var softDeletableType = typeof(ISoftDeletable);
        var mutableEntities = modelBuilder.Model.GetEntityTypes()
            .Where(me => me.ClrType.IsAssignableTo(softDeletableType));
        
        foreach (var mutableEntity in mutableEntities)
        {
            var entityType = mutableEntity.ClrType;
         
            var deletedPropertyName = nameof(ISoftDeletable.Deleted);
            var deletedProperty = entityType.GetProperty(deletedPropertyName)!;
            
            var parameter = Expression.Parameter(entityType);
            var body = Expression.Equal(
                Expression.Property(parameter, deletedProperty),
                Expression.Constant(null, deletedProperty.PropertyType));
            var lambdaExpression = Expression.Lambda(body, parameter);
            
            lambdaExpression.Compile();

            mutableEntity.SetQueryFilter(lambdaExpression);
        }
    }
}