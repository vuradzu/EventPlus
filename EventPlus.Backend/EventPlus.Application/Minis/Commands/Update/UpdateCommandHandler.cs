using EventPlus.Application.Minis.Base;
using EventPlus.Core.Exceptions;
using EventPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Commands.Update;

public class UpdateCommandHandler(IServiceProvider serviceProvider)
    : MinisHandler<UpdateCommandRequest>(serviceProvider)
{
    public override async Task Handle(UpdateCommandRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;
                
        var command = await Database.Set<Command>()
            .Where(c => c.Id == request.Id)
            .FirstOrDefaultAsync(ct);
        
        if (command is null) throw new NotFoundException("No such Command");
        
        var isMember = await Database.Set<CommandMember>()
            .Where(cm => cm.CommandId == request.Id)
            .Where(cm => cm.AppUserId == userId)
            .FirstOrDefaultAsync(ct) is not null;
        
        if (!isMember) throw new PermissionsException();

        command.Name = request.Name;
        command.Description = request.Description;
        command.Avatar = request.Avatar;
        
        await Database.SaveChangesAsync(ct);
    }
}