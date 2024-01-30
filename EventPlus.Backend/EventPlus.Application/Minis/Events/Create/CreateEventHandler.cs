using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Core.Exceptions;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Events.Create;

public class CreateEventHandler(IServiceProvider serviceProvider)
    : MinisHandler<CreateEventRequest, EventModel>(serviceProvider)
{
    public override async Task<EventModel> Handle(CreateEventRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;

        var command = await Database.Set<Command>().FirstOrDefaultAsync(c => c.Id == request.CommandId, ct);

        if (command is null) throw new NotFoundException("No such command");

        if (command.CreatorId != userId) throw new PermissionsException();

        var eventEntity = request.Adapt<Event>();

        var eventEntry = await Database.Set<Event>().AddAsync(eventEntity, ct);

        await Database.SaveChangesAsync(ct);

        return eventEntry.Entity.Adapt<EventModel>();
    }
}