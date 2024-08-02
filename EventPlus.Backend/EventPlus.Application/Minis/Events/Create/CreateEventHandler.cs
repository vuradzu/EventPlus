using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Core.Exceptions;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Events.Create;

public class CreateEventHandler(IServiceProvider serviceProvider)
    : MinisHandler<CreateEventRequest, EventModelMini>(serviceProvider)
{
    protected override async Task<EventModelMini> Process(CreateEventRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;
        var command = await Database.Set<Command>().FirstOrDefaultAsync(c => c.Id == request.CommandId, ct);

        if (command is null) throw new NotFoundException("No such command");
        if (command.CreatorId != userId) throw new PermissionsException();

        var eventEntity = request.Adapt<Event>();
        eventEntity.CreatorId = userId;

        var eventEntry = await Database.Set<Event>().AddAsync(eventEntity, ct);
        await Database.SaveChangesAsync(ct);

        return eventEntry.Entity.Adapt<EventModelMini>();
    }
}