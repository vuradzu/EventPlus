using EventPlus.Domain.Entities.Authorization;
using EventPlus.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations.Authorization;

public class CommandRoleConfiguration: IEntityTypeConfiguration<CommandRole>
{
    public void Configure(EntityTypeBuilder<CommandRole> builder)
    {
        builder.HasKey(cr => cr.Id);

        builder.Property(cr => cr.Title).AsSmallText();
    }
}