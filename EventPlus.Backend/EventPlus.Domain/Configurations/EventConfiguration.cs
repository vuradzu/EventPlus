using EventPlus.Domain.Entities;
using EventPlus.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(e => e.Title).AsSmallText();
        builder.Property(e => e.Description).AsLargeText();
        builder.Property(e => e.Icon).AsLargeText();

        builder.HasOne(e => e.Command)
            .WithMany(c => c.Events)
            .HasForeignKey(e => e.CommandId)
            // TODO: figure out what is wrong with CommandId
            .OnDelete(DeleteBehavior.NoAction);
    }
}