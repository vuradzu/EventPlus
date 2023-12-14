using EventPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations;

public class CommandMemberConfiguration: IEntityTypeConfiguration<CommandMember>
{
    public void Configure(EntityTypeBuilder<CommandMember> builder)
    {
        builder.HasOne(cm => cm.Command)
            .WithMany(c => c.CommandMembers)
            .HasForeignKey(cm => cm.CommandId)
            //TODO: figure out what is wrong with CommandId
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(cm => cm.AppUser)
            .WithMany()
            .HasForeignKey(cm => cm.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}