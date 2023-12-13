using EventPlus.Domain.Entities.Identity;
using EventPlus.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations.Identity;

internal class AppRoleClaimConfiguration : IEntityTypeConfiguration<AppRoleClaim>
{
    public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
    {
        builder.ToTable($"{nameof(AppRoleClaim)}s").HasKey(e => e.Id);

        builder.Property(e => e.ClaimType).AsSmallText();
        builder.Property(e => e.ClaimValue).AsLargeText();

        builder.HasOne(e => e.Role).WithMany(e => e.RoleClaims)
            .HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.Cascade);
    }
}