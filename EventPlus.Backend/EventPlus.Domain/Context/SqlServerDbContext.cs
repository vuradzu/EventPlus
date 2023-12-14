using EventPlus.Domain.Conversions;
using EventPlus.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Extensions;

namespace EventPlus.Domain.Context;

public partial class SqlServerDbContext(DbContextOptions options)
    : IdentityDbContext<AppUser, AppRole, long, AppUserClaim,
        AppUserRole, AppUserLogin, AppRoleClaim, AppToken>(options), ISqlServerDatabase
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyEntityDating(o => o.DateTimeKind = DateTimeKind.Utc);
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