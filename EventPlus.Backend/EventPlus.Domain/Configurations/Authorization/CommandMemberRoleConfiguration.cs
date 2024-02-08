using EventPlus.Domain.Entities.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations.Authorization;

public class CommandMemberRoleConfiguration: IEntityTypeConfiguration<CommandMemberRole>
{
    public void Configure(EntityTypeBuilder<CommandMemberRole> builder)
    {
        builder.HasKey(cmr => cmr.Id);
        
        builder.HasOne(cmr => cmr.Member)
            .WithMany(cm => cm.CommandMemberRoles)
            .HasForeignKey(cmr => cmr.MemberId);

        builder.HasOne(cmr => cmr.Role)
            .WithMany()
            .HasForeignKey(cmr => cmr.RoleId);
    }
}