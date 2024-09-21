using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Users.CheckUsername;

public class CheckUsernameRequest(string username) : IMinisRequest<CheckUsernameResult>
{
    public string Username { get; set; } = username;
}

public class CheckUsernameValidator : AbstractValidator<CheckUsernameRequest>
{
    public CheckUsernameValidator()
    {
        RuleFor(m => m.Username).NotNull().NotEmpty().WithMessage("Username required");
    }
}