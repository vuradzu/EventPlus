using EventPlus.Domain.Context;
using EventPlus.Tests.Configuration;

namespace EventPlus.Tests.Base;

public abstract class Test(string databaseName)
{
    protected readonly ISqlServerDatabase Database = DatabaseConfiguration.GetDatabaseMock(databaseName);
}