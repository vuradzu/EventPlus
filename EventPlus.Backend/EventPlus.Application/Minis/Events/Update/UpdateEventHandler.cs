using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Events.Update;

public class UpdateEventHandler(IServiceProvider serviceProvider)
    : MinisHandler<UpdateEventRequest>(serviceProvider)
{
    protected override async Task Process(UpdateEventRequest request, CancellationToken ct)
    {
        var @event = await Database.Set<Event>().FirstOrDefaultAsync(e => e.Id == request.Id, ct);

        if (@event is null) throw new NotFoundException("No such Event");
        
        @event.Title = request.Title;
        @event.Description = request.Description;
        @event.Priority = request.Priority;
        @event.Date = request.Date;

        await Database.SaveChangesAsync(ct);
    }
}