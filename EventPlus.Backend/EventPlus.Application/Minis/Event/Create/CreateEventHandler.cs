using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Domain.Entities;
using EventPlus.Domain.Entities.Identity;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Application.Minis.Events.Create;

public class CreateEventHandler(IServiceProvider serviceProvider) 
    : MinisHandler<CreateEventRequest, EventModel>(serviceProvider)
{
    public override async Task<EventModel> Handle(CreateEventRequest request, CancellationToken ct)
    {
        // var userId = UserProvider.UserId;
        var userId = 1;
        
        var eventEntity = request.Adapt<Event>();
        
        var eventEntry = await Database.Set<Event>().AddAsync(eventEntity, ct);

        await Database.SaveChangesAsync(ct);

        return eventEntry.Entity.Adapt<EventModel>();
    }
}