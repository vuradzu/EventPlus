using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Invite.Models;
using EventPlus.Core.Exceptions;
using EventPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Commands.Invite.Create;

public class CreateInviteHandler(IServiceProvider serviceProvider)
    : MinisHandler<CreateInviteRequest, InviteCodeModel>(serviceProvider)
{
    public override async Task<InviteCodeModel> Handle(CreateInviteRequest request, CancellationToken ct)
    {
        // var userId = UserProvider.UserId;
        var userId = 1;

        var command = await Database.Set<Command>()
            .SingleOrDefaultAsync(c => c.Id == request.CommandId, ct);

        if (command is null)
            throw new NotFoundException("No such command");

        if (command.CreatorId != userId)
        {
            throw new PermissionsException();
        }

        var code = GenerateCode();

        var codeEntity = new InviteCode
        {
            Code = code,
            UsersAllowed = request.UsersAllowed,
            ValidUntil = request.ValidUntil,
            CommandId = request.CommandId,
            CreatorId = userId
        };

        var codeEntry = await Database.Set<InviteCode>().AddAsync(codeEntity, ct);
        await Database.SaveChangesAsync(ct);

        return new InviteCodeModel
        {
            Code = code,
            CreatedBy = userId,
            ValidUntil = codeEntry.Entity.ValidUntil
        };
    }

    private string GenerateCode()
    {
        var digits = "1234567890";
        var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        var random = new Random();
        var number = random.Next(100000000, 1000000000).ToString();

        var code = number.Select(digit => digit % 2 == 0)
            .Select(isNumber => isNumber
                ? digits.ElementAt(random.Next(0, digits.Length))
                : letters.ElementAt(random.Next(0, letters.Length)))
            .Aggregate(string.Empty, (current, @char) => current + @char);

        return code;
    }
}