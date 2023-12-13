using EventPlus.Domain.Entities.Identity;
using EventPlus.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations.Identity;

internal class AppUserLoginConfiguration : IEntityTypeConfiguration<AppUserLogin>
{
    public void Configure(EntityTypeBuilder<AppUserLogin> builder)
    {
        builder.ToTable($"{nameof(AppUserLogin)}s");

        builder.Property(e => e.LoginProvider).AsLargeText();
        builder.Property(e => e.ProviderKey).AsHugeText();
        builder.Property(e => e.ProviderDisplayName).AsText();
    }
}