using EventPlus.Application.Minis.Base;
using EventPlus.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Users.Avatar;

public class SetUserAvatarRequest : IMinisRequest<string>
{
    [FromForm] public required IFormFile? Image { get; init; }
    [FromQuery] public string? Link { get; init; }
}

public class SetUserAvatarValidator : AbstractValidator<SetUserAvatarRequest>
{
    public SetUserAvatarValidator()
    {
        When(i => i.Image is not null, () =>
        {
            RuleFor(i => i.Image.Length).NotNull().NotEmpty().LessThanOrEqualTo(ImageValidator.IMAGE_MAX_FILE_SIZE)
                .WithMessage("Image size is larger than allowed 10 MB");

            RuleFor(i => i.Image.ContentType).NotNull().NotEmpty()
                .Must(ct => ImageValidator.ALLOWED_IMAGE_CONTENT_TYPES.Any(ct.Equals))
                .WithMessage("Allowed image types: " +
                             string.Join(", ",
                                 ImageValidator.ALLOWED_IMAGE_CONTENT_TYPES.Select(t => $"'{t.Split('/')[1]}'")));
        });
    }
}