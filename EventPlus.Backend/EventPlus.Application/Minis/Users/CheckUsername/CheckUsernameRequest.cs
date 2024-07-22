using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Users.CheckUsername;

public record CheckUsernameRequest(string Username) : IMinisRequest<CheckUsernameResult>;

public class CheckUsernameValidator : AbstractValidator<CheckUsernameRequest>
{
    public CheckUsernameValidator()
    {
        RuleFor(m => m.Username).NotNull().NotEmpty().WithMessage("Username required");
    }
}