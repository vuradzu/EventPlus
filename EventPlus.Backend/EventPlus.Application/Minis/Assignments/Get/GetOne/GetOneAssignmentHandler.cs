using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Assignments.Get.GetOne;

public class GetOneAssignmentHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetOneAssignmentRequest, AssignmentModel>(serviceProvider)
{
    protected override async Task<AssignmentModel> Process(GetOneAssignmentRequest request, CancellationToken ct)
    {
        var assignment = await Database.Set<Assignment>()
            .FirstOrDefaultAsync(c => c.Id == request.Id, ct);
        
        if (assignment is null) throw new NotFoundException("No such Assignment");

        return assignment.Adapt<AssignmentModel>();
    }
}