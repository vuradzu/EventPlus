using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Assignments.Get.GetOne;

public class GetOneAssignmentHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetOneAssignmentRequest, AssignmentsModel>(serviceProvider)
{
    protected override async Task<AssignmentsModel> Process(GetOneAssignmentRequest request, CancellationToken ct)
    {
        var assignment = await Database.Set<Assignment>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, ct);
        
        if (assignment is null) throw new NotFoundException("No such Event");

        return assignment.Adapt<AssignmentsModel>();
    }
}