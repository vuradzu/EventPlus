using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Assignments.Get.GetAll;

public class GetAllAssignmentsHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetAllAssignmentsRequest, ICollection<AssignmentModel>>(serviceProvider)
{
    protected override async Task<ICollection<AssignmentModel>> Process(GetAllAssignmentsRequest request, CancellationToken ct)
    {
        var @event = await Database.Set<Event>().FirstOrDefaultAsync(e => e.Id == request.EventId, ct);

        if (@event is null) throw new NotFoundException("No such Event");

        var assignments = await Database.Set<Assignment>().Where(a => a.EventId == request.EventId).ToArrayAsync(ct);

        return assignments.Adapt<ICollection<AssignmentModel>>();
    }
}