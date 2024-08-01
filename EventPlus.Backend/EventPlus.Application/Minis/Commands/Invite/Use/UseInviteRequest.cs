using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Commands.Invite.Use;

public class UseInviteRequest(string code) : IMinisRequest<UseInviteResult>
{
    public string Code { get; init; } = code;
}

public class UseInviteValidator : AbstractValidator<UseInviteRequest>
{
    public UseInviteValidator()
    {
        RuleFor(ui => ui.Code).MinimumLength(5).MaximumLength(5).WithMessage("Invite code must be 5 symbols length");
    }
}