using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Application.Services;
using EventPlus.Core.Constants;
using EventPlus.Domain.Entities;
using EventPlus.Domain.Entities.Authorization;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Application.Minis.Commands.Create;

public class CreateCommandHandler(IServiceProvider serviceProvider, IJwtService jwtService)
    : MinisHandler<CreateCommandRequest, CreateCommandResult>(serviceProvider)
{
    protected override async Task<CreateCommandResult> Process(CreateCommandRequest request, CancellationToken ct)
    {
        var user = await UserProvider.GetUserAsync();

        var commandEntity = request.Adapt<Command>();

        var adminRole = await Database.Set<CommandRole>().SingleAsync(r => r.Title == CommandRoles.Admin, ct);

        commandEntity.CreatorId = user.Id;
        commandEntity.CommandMembers = new List<CommandMember>
        {
            new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AppUserId = user.Id,
                Avatar = user.Avatar,
                CommandMemberRoles = [new CommandMemberRole { RoleId = adminRole.Id }]
            }
        };
        commandEntity.LastActivity = DateTime.UtcNow;

        var commandEntry = await Database.Set<Command>().AddAsync(commandEntity, ct);

        await Database.SaveChangesAsync(ct);

        var result = commandEntry.Entity.Adapt<CreateCommandResult>();
        var tokens = await jwtService.GenerateAsync(user, commandEntity.Id, ct);
        result.Tokens = tokens.Adapt<CommandTokensModel>();

        return result;
    }
}