using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Mapster;
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
        
        assignment = request.Adapt<Assignment>();
        
        await Database.SaveChangesAsync(ct);
    }
}