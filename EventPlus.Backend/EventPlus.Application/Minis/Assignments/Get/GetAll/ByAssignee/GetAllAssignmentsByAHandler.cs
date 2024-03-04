using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using EventPlus.Domain.Entities.Identity;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Assignments.Get.GetAll.ByAssignee;

public class GetAllAssignmentsByAHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetAllAssignmentsByARequest, ICollection<AssignmentModel>>(serviceProvider)
{
    protected override async Task<ICollection<AssignmentModel>> Process(GetAllAssignmentsByARequest request, CancellationToken ct)
    {
        var user = await Database.Set<AppUser>().FirstOrDefaultAsync(e => e.Id == request.AssigneeId, ct);

        if (user is null) throw new NotFoundException("No such User");
        
        var assignments = await Database.Set<Assignment>().Where(a => a.AssigneeId == request.AssigneeId).ToArrayAsync(ct);

        return assignments.Adapt<ICollection<AssignmentModel>>();
    }
}