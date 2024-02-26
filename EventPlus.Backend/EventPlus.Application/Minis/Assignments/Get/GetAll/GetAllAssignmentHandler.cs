using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Assignments.Get.GetAll;

public class GetAllAssignmentHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetAllAssignmentRequest, ICollection<AssignmentsModel>>(serviceProvider)
{
    protected override async Task<ICollection<AssignmentsModel>> Process(GetAllAssignmentRequest request, CancellationToken ct)
    {
        var @event = await Database.Set<Event>().FirstOrDefaultAsync(e => e.Id == request.EventId, ct);

        if (@event is null) throw new NotFoundException("No such Event");

        var events = await Database.Set<Assignment>().Where(a => a.EventId == request.EventId).ToListAsync(ct);

        return events.Adapt<ICollection<AssignmentsModel>>();
    }
}