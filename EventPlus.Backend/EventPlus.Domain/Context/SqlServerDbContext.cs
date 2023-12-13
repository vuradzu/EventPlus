using EventPlus.Domain.Conversions;
using EventPlus.Domain.Entities.Base;
using EventPlus.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NeerCore.Data.EntityFramework.Extensions;

namespace EventPlus.Domain.Context;

public partial class SqlServerDbContext : IdentityDbContext<AppUser, AppRole, long, AppUserClaim,
    AppUserRole, AppUserLogin, AppRoleClaim, AppToken>, ISqlServerDatabase
{
    public SqlServerDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyEntityDating(options => options.DateTimeKind = DateTimeKind.Utc);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        builder.AddLocalizedStrings(GetType().Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.ApplyLocalizedStringConversions();
        builder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConvertor>();
        // builder.Properties<Vote>().HaveConversion<EnumToStringConverter<Vote>>();
    }
}