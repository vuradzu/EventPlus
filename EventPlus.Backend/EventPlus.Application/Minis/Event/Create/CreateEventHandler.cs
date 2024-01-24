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
        var user = await Database.Set<AppUser>().FirstAsync(u => u.Id == 1, ct);
        
        var eventEntity = request.Adapt<Event>();
        eventEntity.CreatorId = user.Id;
        eventEntity.EventMembers = new List<EventMember>
        {
            new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AppUserId = user.Id,
                Avatar = user.Avatar
            }
        };
        
        var commandEntry = await Database.Set<Event>().AddAsync(eventEntity, ct);

        await Database.SaveChangesAsync(ct);

        return commandEntry.Entity.Adapt<EventModel>();
    }
}