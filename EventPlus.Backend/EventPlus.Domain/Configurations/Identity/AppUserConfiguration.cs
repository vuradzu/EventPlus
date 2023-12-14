using EventPlus.Domain.Entities.Identity;
using EventPlus.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations.Identity;

internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable($"{nameof(AppUser)}s").HasKey(e => e.Id);

        builder.Property(e => e.UserName).AsText();
        builder.Property(e => e.NormalizedUserName).AsText();
        builder.Property(e => e.Description).AsHugeText();
        builder.Property(e => e.SecurityStamp).AsText();
        builder.Property(e => e.ConcurrencyStamp).AsText();
        builder.Property(e => e.PhoneNumber).AsSmallText();
        builder.Property(e => e.Registered).HasDefaultValueUtcDateTime();

        builder.HasMany(e => e.UserRoles).WithOne(e => e.User)
            .HasForeignKey(e => e.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(e => e.UserClaims).WithOne(e => e.User)
            .HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}