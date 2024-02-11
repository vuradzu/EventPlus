using EventPlus.Domain.Entities.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations.Authorization;

public class CommandMemberPermissionConfiguration: IEntityTypeConfiguration<CommandMemberPermission>
{
    public void Configure(EntityTypeBuilder<CommandMemberPermission> builder)
    {
        builder.HasKey(cmp => cmp.Id);

        builder.HasOne(cmp => cmp.Member)
            .WithMany(cm => cm.CommandMemberPermissions)
            .HasForeignKey(cmp => cmp.MemberId);

        builder.HasOne(cmp => cmp.Permission)
            .WithMany()
            .HasForeignKey(cmp => cmp.PermissionId);
    }
}