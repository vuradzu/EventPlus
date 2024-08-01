using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Events.Get.GetAll;

public class GetAllEventsHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetAllEventsRequest, ICollection<EventModelMini>>(serviceProvider)
{
    protected override async Task<ICollection<EventModelMini>> Process(GetAllEventsRequest request, CancellationToken ct)
    {
        var command = await Database.Set<Command>().FirstOrDefaultAsync(e => e.Id == request.CommandId, ct);

        if (command is null) throw new NotFoundException("No such command");

        var events = await Database.Set<Event>()
            .Include(e => e.Assignments)
            .Where(e => e.CommandId == request.CommandId)
            .ToListAsync(ct);
        
        return events.Adapt<ICollection<EventModelMini>>();
    }
}