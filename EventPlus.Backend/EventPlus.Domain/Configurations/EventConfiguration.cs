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
        builder.Property(c => c.Description).AsLargeText();
    }
}