using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Core.Exceptions;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Events.Get.GetAll;

public class GetAllEventsHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetAllEventsRequest, ICollection<EventModel>>(serviceProvider)
{
    public override async Task<ICollection<EventModel>> Handle(GetAllEventsRequest request, CancellationToken ct)
    {
        var userId = UserProvider.UserId;
        
        var command = await Database.Set<Command>().FirstOrDefaultAsync(e => e.Id == request.CommandId, ct);
        
        var isMember = await Database.Set<CommandMember>()
            .Where(cm => cm.CommandId == request.CommandId)
            .Where(cm => cm.AppUserId == userId)
            .FirstOrDefaultAsync(ct) is not null;
        
        if (command is null) throw new NotFoundException("No such command");
        if (!isMember) throw new PermissionsException();
        
        var events = await Database.Set<Event>().Where(e => e.CommandId == request.CommandId).ToListAsync(ct);
        
        return events.Adapt<ICollection<EventModel>>();
    }
}