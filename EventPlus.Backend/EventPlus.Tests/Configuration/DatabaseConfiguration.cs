using EventPlus.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MockQueryable.Moq;
using Moq;
using NeerCore.Data.Abstractions;

namespace EventPlus.Tests.Configuration;

public static class DatabaseConfiguration
{
    public static ISqlServerDatabase GetDatabaseMock(string databaseName)
    {
        var contextOptions = new DbContextOptionsBuilder<SqlServerDbContext>()
            .UseInMemoryDatabase(databaseName)
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var context = new SqlServerDbContext(contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }

    [Obsolete]
    public static void MockCollectionAsDbSet<T>(this Mock<ISqlServerDatabase> databaseMock, ICollection<T> collection)
        where T : class, IEntity
    {
        var dbSetMock = collection.ToList().BuildMock().BuildMockDbSet();

        databaseMock.Setup(x => x.Set<T>()).Returns(dbSetMock.Object);
    }
}