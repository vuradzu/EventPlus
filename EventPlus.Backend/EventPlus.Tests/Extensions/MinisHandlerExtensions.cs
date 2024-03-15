using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Context;
using MockQueryable.Moq;
using Moq;
using NeerCore.Data.Abstractions;

namespace EventPlus.Tests.Extensions;

public static class MinisHandlerExtensions
{
    #region MockDatabase
    
    /// <summary>
    /// Mock database for current handler
    /// </summary>
    /// <param name="handler">The handler.</param>
    /// <param name="databaseMock">The database mock.</param>
    /// <typeparam name="T">The request type without result</typeparam>
    [Obsolete("Use overload with ISqlServerDatabase parameter, not with the mock")]
    public static void MockDatabase<T>(this MinisHandler<T> handler,
        Mock<ISqlServerDatabase> databaseMock)
        where T : IMinisRequest
    {
        handler.Database = databaseMock.Object;
    }

    /// <summary>
    /// Mock database for current handler
    /// </summary>
    /// <param name="handler">The handler.</param>
    /// <param name="databaseMock">The database mock.</param>
    /// <typeparam name="T">The request type with result</typeparam>
    /// <typeparam name="TK">The result</typeparam>
    [Obsolete("Use overload with ISqlServerDatabase parameter, not with the mock")]
    public static void MockDatabase<T, TK>(this MinisHandler<T, TK> handler,
        Mock<ISqlServerDatabase> databaseMock)
        where T : IMinisRequest<TK>
    {
        handler.Database = databaseMock.Object;
    }
    
    /// <summary>
    /// Mock database for current handler
    /// </summary>
    /// <param name="handler">The handler.</param>
    /// <param name="databaseMock">The database mock.</param>
    /// <typeparam name="T">The request type without result</typeparam>
    public static void MockDatabase<T>(this MinisHandler<T> handler,
        ISqlServerDatabase databaseMock)
        where T : IMinisRequest
    {
        handler.Database = databaseMock;
    }

    /// <summary>
    /// Mock database for current handler
    /// </summary>
    /// <param name="handler">The handler.</param>
    /// <param name="databaseMock">The database mock.</param>
    /// <typeparam name="T">The request type with result</typeparam>
    /// <typeparam name="TK">The result</typeparam>
    public static void MockDatabase<T, TK>(this MinisHandler<T, TK> handler,
        ISqlServerDatabase databaseMock)
        where T : IMinisRequest<TK>
    {
        handler.Database = databaseMock;
    }

    #endregion

    #region MockDbSets
    
    /// <summary>
    /// Extended version of Mock Database method. This method will create new mock of the database, add collections
    /// as DbSets to it, and apply database mock to the handler.
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <param name="collections">Array of collections that should be used as DbSets</param>
    /// <typeparam name="T">The handler request type without result</typeparam>
    /// <typeparam name="TK">The entity type. Must be inherited from <see cref="IEntity">IEntity</see></typeparam>
    public static void MockDbSets<T, TK>(this MinisHandler<T> handler,
        params ICollection<TK>[]? collections)
        where T : IMinisRequest
        where TK : class, IEntity
    {
        handler.MockDbSets(new Mock<ISqlServerDatabase>(), collections);
    }

    /// <summary>
    /// Extended version of Mock Database method. This method will use provided mock of the database, add collections
    /// as DbSets to it, and apply database mock to the handler.
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <param name="databaseCache">The database mock</param>
    /// <param name="collections">Array of collections that should be used as DbSets</param>
    /// <typeparam name="T">The handler request type without result</typeparam>
    /// <typeparam name="TK">The entity type. Must be inherited from <see cref="IEntity">IEntity</see></typeparam>
    public static void MockDbSets<T, TK>(this MinisHandler<T> handler,
        Mock<ISqlServerDatabase> databaseCache,
        params ICollection<TK>[]? collections)
        where T : IMinisRequest
        where TK : class, IEntity
    {
        collections ??= [];

        foreach (var collection in collections)
            RegisterCollectionAsDbSet(databaseCache, collection);

        handler.Database = databaseCache.Object;
    }

    /// <summary>
    /// Extended version of Mock Database method. This method will create new mock of the database, add collections
    /// as DbSets to it, and apply database mock to the handler.
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <param name="collections">Array of collections that should be used as DbSets</param>
    /// <typeparam name="T">The handler request type with result</typeparam>
    /// <typeparam name="TK">The handler result type</typeparam>
    /// <typeparam name="TL">The entity type. Must be inherited from <see cref="IEntity">IEntity</see></typeparam>
    public static void MockDbSets<T, TK, TL>(this MinisHandler<T, TK> handler,
        params ICollection<TL>[]? collections)
        where T : IMinisRequest<TK>
        where TL : class, IEntity
    {
        handler.MockDbSets(new Mock<ISqlServerDatabase>(), collections);
    }

    /// <summary>
    /// Extended version of Mock Database method. This method will use provided mock of the database, add collections
    /// as DbSets to it, and apply database mock to the handler.
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <param name="databaseCache">The database mock</param>
    /// <param name="collections">Array of collections that should be used as DbSets</param>
    /// <typeparam name="T">The handler request type with result</typeparam>
    /// <typeparam name="TK">The handler result type</typeparam>
    /// <typeparam name="TL">The entity type. Must be inherited from <see cref="IEntity">IEntity</see></typeparam>
    public static void MockDbSets<T, TK, TL>(this MinisHandler<T, TK> handler,
        Mock<ISqlServerDatabase> databaseCache,
        params ICollection<TL>[]? collections)
        where T : IMinisRequest<TK>
        where TL : class, IEntity
    {
        collections ??= [];

        foreach (var collection in collections)
            RegisterCollectionAsDbSet(databaseCache, collection);

        handler.Database = databaseCache.Object;
    }

    private static void RegisterCollectionAsDbSet<T>(Mock<ISqlServerDatabase> databaseMock, ICollection<T> collection)
        where T : class, IEntity
    {
        var collectionDbSetMock = collection.ToList().BuildMock().BuildMockDbSet();
        databaseMock.Setup(x => x.Set<T>()).Returns(collectionDbSetMock.Object);
    }
    
    #endregion
}