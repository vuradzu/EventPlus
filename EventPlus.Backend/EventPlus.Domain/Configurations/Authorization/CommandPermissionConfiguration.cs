using EventPlus.Domain.Entities.Authorization;
using EventPlus.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations.Authorization;

public class CommandPermissionConfiguration: IEntityTypeConfiguration<CommandPermission>
{
    public void Configure(EntityTypeBuilder<CommandPermission> builder)
    {
        builder.HasKey(cp => cp.Id);

        builder.Property(cp => cp.Title).AsSmallText();
    }
}