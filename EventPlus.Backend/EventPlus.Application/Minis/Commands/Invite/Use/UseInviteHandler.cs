using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Application.Services;
using EventPlus.Core.Constants;
using EventPlus.Core.Extensions;
using EventPlus.Domain.Entities;
using EventPlus.Domain.Entities.Authorization;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Application.Minis.Commands.Invite.Use;

public class UseInviteHandler(IServiceProvider serviceProvider, IJwtService jwtService)
    : MinisHandler<UseInviteRequest, UseInviteResult>(serviceProvider)
{
    protected override async Task<UseInviteResult> Process(UseInviteRequest request, CancellationToken ct)
    {
        var result = new UseInviteResult { IsSuccess = false };
        var user = await UserProvider.GetUserAsync();

        var invite = await Database.Set<InviteCode>()
            .SingleOrDefaultAsync(ic => ic.Code == request.Code, ct);


        if (invite is null)
        {
            result.Message = "No such invitation";
            return result;
        }

        if (invite.ValidUntil.UtcDateTime < DateTimeOffset.UtcNow)
        {
            result.Message = "Invitation has already been expired";
            return result;
        }

        if (await Database.Set<CommandMember>()
                .FirstOrDefaultAsync(cm => cm.AppUserId == user.Id
                                           && cm.CommandId == invite.CommandId, ct) is not null)
        {
            result.Message = "You are already a member of this group";
            return result;
        }

        invite.UsersAllowed--;
        result.CommandId = invite.CommandId;

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

        var tokens = await jwtService.GenerateAsync(user, result.CommandId, ct);

        result.IsSuccess = true;
        result.Tokens = tokens.Adapt<CommandTokensModel>();

        return result;
    }
}