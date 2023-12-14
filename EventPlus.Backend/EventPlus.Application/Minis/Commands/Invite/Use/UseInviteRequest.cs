using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Commands.Invite.Use;

public class UseInviteRequest(string code) : IMinisRequest
{
    public string Code { get; init; } = code;
}

public class UseInviteValidator : AbstractValidator<UseInviteRequest>
{
    public UseInviteValidator()
    {
        RuleFor(ui => ui.Code).MinimumLength(9).MaximumLength(9).WithMessage("Invite code must be 9 symbols length");
    }
}