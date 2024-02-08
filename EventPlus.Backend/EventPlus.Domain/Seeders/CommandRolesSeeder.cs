using EventPlus.Core.Constants;
using EventPlus.Domain.Entities.Authorization;
using Microsoft.EntityFrameworkCore;
using NeerCore.Data.EntityFramework.Abstractions;

namespace EventPlus.Domain.Seeders;

public class CommandRolesSeeder : IDataSeeder
{
    public void Seed(ModelBuilder builder)
    {
        builder.Entity<CommandRole>(b =>
        {
            b.HasData([
                new CommandRole
                {
                    Id = 1,
                    Title = CommandRoles.Admin,
                }
            ]);
        });
    }
}