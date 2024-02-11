using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Users.Avatar;
using EventPlus.Core.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Commands.Avatar;

public class SetCommandAvatarRequest : IMinisRequest<string>
{
    public required long Id { get; set; }
    
    [FromQuery] public string? Link { get; init; }

    [FromForm] public IFormFile? File { get; init; }
    
}

public class SetCommandAvatarValidator : AbstractValidator<SetUserAvatarRequest>
{
    public SetCommandAvatarValidator()
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