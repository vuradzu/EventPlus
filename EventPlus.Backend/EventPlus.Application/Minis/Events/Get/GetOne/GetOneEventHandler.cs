using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Core.Exceptions;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Events.Get.GetOne;

public class GetOneEventHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetOneEventRequest, EventModel>(serviceProvider)
{
    public override async Task<EventModel> Handle(GetOneEventRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;
        
        var @event = await Database.Set<Event>().FirstOrDefaultAsync(e => e.Id == request.Id, ct);
        
        if (@event is null) throw new NotFoundException("No such Event");
        
        var isMember = await Database.Set<CommandMember>()
            .Where(cm => cm.CommandId == @event.CommandId)
            .Where(cm => cm.AppUserId == userId)
            .FirstOrDefaultAsync(ct) is not null;

        if (!isMember) throw new PermissionsException();

        return @event.Adapt<EventModel>();
    }
}