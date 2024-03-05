using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Users.Models;
using EventPlus.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Application.Minis.Commands.Get.GetMembers;

public class GetCommandMembersHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetCommandMembersRequest, ICollection<AppUserModelMinified>>(serviceProvider)
{
    protected override async Task<ICollection<AppUserModelMinified>> Process(GetCommandMembersRequest request, CancellationToken ct)
    {
        var members = await Database.Set<CommandMember>()
            .Where(cm => cm.CommandId == request.Id)
            .Include(cm => cm.AppUser)
            .Select(m => m.AppUser)
            .ProjectToType<AppUserModelMinified>()
            .ToArrayAsync(ct);
        
        return members;
    }
}