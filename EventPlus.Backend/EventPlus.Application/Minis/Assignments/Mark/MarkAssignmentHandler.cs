
using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Assignments.Mark;

public class MarkAssignmentHandler(IServiceProvider serviceProvider)
    : MinisHandler<MarkAssignmentRequest>(serviceProvider)
{
    protected override async Task Process(MarkAssignmentRequest request, CancellationToken ct)
    {
        var assignment = await Database.Set<Assignment>().FirstOrDefaultAsync(e => e.Id == request.Id, ct);

        if (assignment is null) throw new NotFoundException("No such Assignment");
        
        assignment.Completed = request.Completed;
        
        await Database.SaveChangesAsync(ct);
    }
}