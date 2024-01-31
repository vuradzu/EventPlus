using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Events.Delete;

public class DeleteEventHandler(IServiceProvider serviceProvider)
    : MinisHandler<DeleteEventRequest>(serviceProvider)
{
    public override async Task Handle(DeleteEventRequest request, CancellationToken ct)
    {
        var @event = await Database.Set<Event>().FirstOrDefaultAsync(e => e.Id == request.Id, ct);
        
        if (@event is null)
            throw new NotFoundException("No such event");
        
        Database.Remove(@event);
        await Database.SaveChangesAsync(ct);
    }
}