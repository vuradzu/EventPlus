using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Design;

namespace EventPlus.Domain.Context;

public class SqlServerDbContextFactory : DbContextFactoryBase<SqlServerDbContext>
{
    public override TextWriter? LogWriter => null;
    public override string SelectedConnectionName => "Default";

    public override string[] SettingsPaths => new[]
    {
        "appsettings.Local.json", // for project
        "../../app/EventPlus.Api/appsettings.Local.json" // relative path for migrations
    };

    public override SqlServerDbContext CreateDbContext(string[] args) => new(CreateContextOptions());


    public override void ConfigureContextOptions(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(ConnectionString,
            options => options.MigrationsAssembly(MigrationsAssembly));
}