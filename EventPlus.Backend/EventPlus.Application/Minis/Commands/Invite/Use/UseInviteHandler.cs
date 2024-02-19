using EventPlus.Application.Minis.Base;
using EventPlus.Core.Constants;
using EventPlus.Core.Extensions;
using EventPlus.Domain.Entities;
using EventPlus.Domain.Entities.Authorization;
using EventPlus.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Commands.Invite.Use;

public class UseInviteHandler(IServiceProvider serviceProvider) : MinisHandler<UseInviteRequest>(serviceProvider)
{
    protected override async Task Process(UseInviteRequest request, CancellationToken ct)
    {
        // var user = await UserProvider.GetUserAsync();
        var user = await Database.Set<AppUser>().FirstAsync(u => u.Id == 1, ct);

        var invite = await Database.Set<InviteCode>()
            .SingleOrDefaultAsync(ic => ic.Code == request.Code, ct);


        if (invite is null || invite.ValidUntil.UtcDateTime < DateTimeOffset.UtcNow)
            throw new ValidationFailedException("Invitation has already been expired");

        if (await Database.Set<CommandMember>()
                .FirstOrDefaultAsync(cm => cm.AppUserId == user.Id
                                           && cm.CommandId == invite.CommandId, ct) is not null)
            throw new ValidationFailedException("You already are a member of this group");


        invite.UsersAllowed--;

        if (invite.UsersAllowed == 0)
            Database.Set<InviteCode>().Remove(invite);

        var memberPermission = await Database.Set<CommandPermission>()
            .SingleAsync(r => r.Title == CommandPermissions.CommandMember.GetPermissionInfo().Key, ct);

        var newMemberEntity = new CommandMember
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Avatar = user.Avatar,
            AppUserId = user.Id,
            CommandId = invite.CommandId,
            CommandMemberPermissions = [new CommandMemberPermission { PermissionId = memberPermission.Id }]
        };

        await Database.Set<CommandMember>().AddAsync(newMemberEntity, ct);
        await Database.SaveChangesAsync(ct);
    }
}