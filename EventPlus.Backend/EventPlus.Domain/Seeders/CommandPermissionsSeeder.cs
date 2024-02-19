using EventPlus.Core.Constants;
using EventPlus.Core.Extensions;
using EventPlus.Domain.Entities.Authorization;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Abstractions;

namespace EventPlus.Domain.Seeders;

public class CommandPermissionsSeeder : IDataSeeder
{
    public void Seed(ModelBuilder builder)
    {
        builder.Entity<CommandPermission>(b =>
        {
            b.HasData([
                
                //Command permissions
                new CommandPermission { Id = 1, Title = CommandPermissions.ManageCommand.GetPermissionInfo().Key},
                new CommandPermission { Id = 2, Title = CommandPermissions.ManageCommandMembers.GetPermissionInfo().Key},
                new CommandPermission { Id = 4, Title = CommandPermissions.CommandMember.GetPermissionInfo().Key},
                
                //Event permissions
                new CommandPermission { Id = 3, Title = CommandPermissions.ManageEvent.GetPermissionInfo().Key},
            ]);
        });
    }
}