using EventPlus.Domain.Entities.Authorization;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Abstractions;

namespace EventPlus.Domain.Seeders;

public class CommandRolePermissionsSeeder : IDataSeeder
{
    public void Seed(ModelBuilder builder)
    {
        builder.Entity<CommandRolePermission>(b =>
        {
            b.HasData([
                new CommandRolePermission { Id = 1, PermissionId = 1, RoleId = 1 },
                new CommandRolePermission { Id = 2, PermissionId = 2, RoleId = 1 },
                new CommandRolePermission { Id = 3, PermissionId = 3, RoleId = 1 },
            ]);
        });
    }
}