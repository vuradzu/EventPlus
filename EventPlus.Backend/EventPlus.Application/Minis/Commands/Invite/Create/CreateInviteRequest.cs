using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Invite.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Commands.Invite.Create;

public record CreateInviteRequest(byte UsersAllowed, DateTimeOffset ValidUntil) : IMinisRequest<InviteCodeModel>
{
    [FromRoute] public long CommandId { get; set; }
}

public class CreateInviteValidator : AbstractValidator<CreateInviteRequest>
{
    public CreateInviteValidator()
    {
        RuleFor(ci => ci.UsersAllowed).Must(ua => ua > 0).WithMessage("Invite users count must be greater than 0");
        RuleFor(ci => ci.ValidUntil).Must(vu => vu.UtcDateTime > DateTimeOffset.UtcNow)
            .WithMessage("Valid time must be greater than current time");
    }
}