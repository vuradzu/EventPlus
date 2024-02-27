using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Assignments.Delete;

public class DeleteAssignmentHandler(IServiceProvider serviceProvider)
    : MinisHandler<DeleteAssignmentRequest>(serviceProvider)
{
    protected override async Task Process(DeleteAssignmentRequest request, CancellationToken ct)
    {
        var assignment = await Database.Set<Assignment>().FirstOrDefaultAsync(c => c.Id == request.Id, ct);
        
        if (assignment is null)
            throw new NotFoundException("No such assignment");
        
        Database.Remove(assignment);
        await Database.SaveChangesAsync(ct);
    }
}