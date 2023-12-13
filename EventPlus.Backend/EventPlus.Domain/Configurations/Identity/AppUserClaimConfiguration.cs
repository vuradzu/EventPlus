using EventPlus.Domain.Entities.Identity;
using EventPlus.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations.Identity;

internal class AppUserClaimConfiguration : IEntityTypeConfiguration<AppUserClaim>
{
    public void Configure(EntityTypeBuilder<AppUserClaim> builder)
    {
        builder.ToTable($"{nameof(AppUserClaim)}s").HasKey(e => e.Id);

        builder.Property(e => e.ClaimType).AsSmallText();
        builder.Property(e => e.ClaimValue).AsLargeText();
    }
}