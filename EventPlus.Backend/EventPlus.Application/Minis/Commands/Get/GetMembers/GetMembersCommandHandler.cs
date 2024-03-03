using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Domain.Entities;
using EventPlus.Domain.Entities.Identity;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Commands.Get.GetMembers;

public class GetMembersCommandHandler(IServiceProvider serviceProvider)
    : MinisHandler<GetMembersCommandReqest, ICollection<AppUser>>(serviceProvider)
{
    protected override async Task<ICollection<AppUser>> Process(GetMembersCommandReqest request, CancellationToken ct)
    {
        var members = await Database.Set<CommandMember>()
            .Where(cm => cm.CommandId == request.Id)
            .Include(cm => cm.AppUser)
            .Select(m => m.AppUser)
            .ProjectToType<AppUser>()
            .ToArrayAsync(ct);
        
        return members;
    }
}