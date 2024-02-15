using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Core.Exceptions;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Commands.Get.GetOne;

public class GetOneCommandHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetOneCommandRequest, CommandModel>(serviceProvider)
{
    protected override async Task<CommandModel> Process(GetOneCommandRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;
                
        var command = await Database.Set<Command>()
            .Where(c => c.Id == request.Id)
            .ProjectToType<CommandModel>()
            .FirstOrDefaultAsync(ct);
        
        if (command is null) throw new NotFoundException("No such Command");
        
        var isMember = await Database.Set<CommandMember>()
            .Where(cm => cm.CommandId == request.Id)
            .Where(cm => cm.AppUserId == userId)
            .FirstOrDefaultAsync(ct) is not null;
        
        if (!isMember) throw new PermissionsException();
        
        return command;
        
    }
}