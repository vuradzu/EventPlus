using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Application.Services;
using EventPlus.Core.Exceptions;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Commands.Get.SwitchCommand;

public class SwitchCommandHandler(IServiceProvider serviceProvider, IJwtService jwtService)
    : MinisHandler<SwitchCommandRequest, SwitchCommandResult>(serviceProvider)
{
    protected override async Task<SwitchCommandResult> Process(SwitchCommandRequest request, CancellationToken ct)
    {
        var user = await UserProvider.GetUserAsync();

        var command = await Database.Set<Command>()
            .Include(c => c.CommandMembers)
            .FirstOrDefaultAsync(c => c.Id == request.Id, ct);

        if (command is null)
            throw new NotFoundException("No such command");

        if (command.CommandMembers!.All(cm => cm.AppUserId != UserProvider.UserId))
            throw new PermissionsException();
        
        command.LastActivity = DateTime.UtcNow;
        await Database.SaveChangesAsync(ct);

        var tokens = await jwtService.GenerateAsync(user, command.Id, ct);

        return new SwitchCommandResult
        {
            Command = command.Adapt<CommandModel>(),
            Tokens = tokens.Adapt<CommandTokensModel>()
        };
    }
}