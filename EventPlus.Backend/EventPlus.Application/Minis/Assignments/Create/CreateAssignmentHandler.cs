using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Mapster;

namespace EventPlus.Application.Minis.Assignments.Create;

public class CreateAssignmentHandler(IServiceProvider serviceProvider)
    : MinisHandler<CreateAssignmentRequest, AssignmentModel>(serviceProvider)
{
    protected override async Task<AssignmentModel> Process(CreateAssignmentRequest request, CancellationToken ct)
    {
        var userId =  UserProvider.UserId;
        
        var assignmentEntity = request.Adapt<Assignment>();

        assignmentEntity.CreatorId = userId;

        assignmentEntity.CanBeCompleted = request.CanBeCompleted ?? true;
        
        var assignmentEntry = await Database.Set<Assignment>().AddAsync(assignmentEntity, ct);

        await Database.SaveChangesAsync(ct);

        return assignmentEntry.Entity.Adapt<AssignmentModel>();
    }
}