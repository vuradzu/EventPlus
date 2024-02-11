using EventPlus.Application.Minis.Base;
using EventPlus.Core.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Commands.Avatar;

public class SetUserAvatarRequest : IMinisRequest<string>
{
    [FromQuery] public string? Link { get; init; }

    [FromForm] public IFormFile? File { get; init; }
}

public class SetUserAvatarValidator : AbstractValidator<SetUserAvatarRequest>
{
    public SetUserAvatarValidator()
    {
        When(sa => sa.Link is not null, () =>
        {
            RuleFor(sa => sa.Link)
                .NotEmpty()
                .WithMessage("Wrong link")
                .Must(l => Assets.ImagesAllowedExtensions.Any(ie =>
                    l.ToLower().EndsWith(ie, StringComparison.OrdinalIgnoreCase)))
                .WithMessage($"Allowed extensions: {string.Join(", ", Assets.ImagesAllowedExtensions)}.");
        });
    }
}