using EventPlus.Domain.Entities;
using EventPlus.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations;

public class CommandConfiguration: IEntityTypeConfiguration<Command>
{
    public void Configure(EntityTypeBuilder<Command> builder)
    {
        builder.Property(c => c.Name).AsSmallText();
        builder.Property(c => c.Description).AsLargeText();
    }
}