using EventPlus.Domain.Entities.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventPlus.Domain.Configurations.Authorization;

public class CommandRolePermissionConfiguration: IEntityTypeConfiguration<CommandRolePermission>
{
    public void Configure(EntityTypeBuilder<CommandRolePermission> builder)
    {
        builder.HasKey(crp => crp.Id);

        builder.HasOne(crp => crp.Role)
            .WithMany(cr => cr.RolePermissions)
            .HasForeignKey(crp => crp.RoleId);
        
        builder.HasOne(crp => crp.Permission)
            .WithMany()
            .HasForeignKey(crp => crp.PermissionId);
    }
}