using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using Mapster;

namespace EventPlus.Application.Minis.Assignments.Create;

public class CreateAssignmentHandler(IServiceProvider serviceProvider)
    : MinisHandler<CreateAssignmentRequest, AssignmentsModel>(serviceProvider)
{
    protected override async Task<AssignmentsModel> Process(CreateAssignmentRequest request, CancellationToken ct)
    {
        var user =  UserProvider.UserId;
        
        var assignmentEntity = request.Adapt<Assignment>();

        assignmentEntity.CreatorId = user;
        
        assignmentEntity.Title = request.Title;
        assignmentEntity.Description = request.Description;
        assignmentEntity.Priority = request.Priority;
        assignmentEntity.Completed = request.Completed;
        assignmentEntity.CanBeCompleted = request.CanBeCompleted;
        assignmentEntity.CompletionTime = request.CompletionTime;
        assignmentEntity.EventId = request.EventId;
        
        assignmentEntity.AssigneId = request.AssigneeId;
     
        
        var assignmentEntry = await Database.Set<Assignment>().AddAsync(assignmentEntity, ct);

        await Database.SaveChangesAsync(ct);

        return assignmentEntry.Entity.Adapt<AssignmentsModel>();
    }
}