using EventPlus.Application.Minis.Base;
using EventPlus.Core.Exceptions;
using EventPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Events.Update;

public class UpdateEventHandler(IServiceProvider serviceProvider)
    : MinisHandler<UpdateEventRequest>(serviceProvider)
{
    public override async Task Handle(UpdateEventRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var @event = await Database.Set<Event>().FirstOrDefaultAsync(e => e.Id == request.Id, ct);
        
        if (@event is null) throw new NotFoundException("No such Event");
        
        var isMember = await Database.Set<CommandMember>()
            .Where(cm => cm.CommandId == @event.CommandId)  
            .Where(cm => cm.AppUserId == userId)
            .FirstOrDefaultAsync(ct) is not null;

        if (!isMember) throw new PermissionsException();

        @event.Title = request.Title;

        @event.Description = request.Description;
        
        @event.Priority = request.Priority;
        
        @event.Date = request.Date;

        await Database.SaveChangesAsync(ct);
    }
}