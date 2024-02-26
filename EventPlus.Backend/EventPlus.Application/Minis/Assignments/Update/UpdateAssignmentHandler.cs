using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Assignments.Update;

public class UpdateAssignmentHandler(IServiceProvider serviceProvider)
    : MinisHandler<UpdateAssignmentRequest>(serviceProvider)
{
    protected override async Task Process(UpdateAssignmentRequest request, CancellationToken ct)
    {
        var assignment = await Database.Set<Assignment>().FirstOrDefaultAsync(e => e.Id == request.Id, ct);

        if (assignment is null) throw new NotFoundException("No such Assignment");

        assignment.Title = request.Title;
        assignment.Description = request.Description;
        assignment.Priority = request.Priority;
        assignment.Date = request.Date;
        assignment.Completed = request.Completed;
        assignment.CanBeCompleted = request.CanBeCompleted;
        assignment.AssigneId = request.AssigneeId;
        assignment.CompletionTime = request.CompletionTime;
        
        await Database.SaveChangesAsync(ct);
    }
}