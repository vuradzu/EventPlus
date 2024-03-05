using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Application.Minis.Assignments.Get.GetAll;

public class FilterAssignmentsHandler(IServiceProvider serviceProvider)
    : MinisHandler<FilterAssignmentsRequest, ICollection<AssignmentModel>>(serviceProvider)
{
    protected override async Task<ICollection<AssignmentModel>> Process(FilterAssignmentsRequest request, CancellationToken ct)
    {
        var assignmentsQuery = Database.Set<Assignment>().AsQueryable();
        
        if (request.UserId is not null)
             assignmentsQuery = assignmentsQuery.Where(aq => aq.AssigneeId == request.UserId);
        
        if (request.EventId is not null)
            assignmentsQuery = assignmentsQuery.Where(a => a.EventId == request.EventId);

        var assignments = await assignmentsQuery.ToArrayAsync(ct);
        
        return assignments.Adapt<ICollection<AssignmentModel>>();
    }
}