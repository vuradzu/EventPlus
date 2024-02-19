using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Events.Get.GetOne;

public class GetOneEventHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetOneEventRequest, EventModel>(serviceProvider)
{
    protected override async Task<EventModel> Process(GetOneEventRequest request, CancellationToken ct)
    {
        var @event = await Database.Set<Event>()
            .FirstOrDefaultAsync(e => e.Id == request.Id, ct);

        if (@event is null) throw new NotFoundException("No such Event");

        return @event.Adapt<EventModel>();
    }
}