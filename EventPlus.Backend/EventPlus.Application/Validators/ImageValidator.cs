using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace EventPlus.Application.Validators;

public class ImageValidator : AbstractValidator<IFormFile>
{
    private const int MEGA_BITES = 5;
    public const int IMAGE_MAX_FILE_SIZE = MEGA_BITES * 1024 * 1024;

    public static readonly string[] ALLOWED_IMAGE_CONTENT_TYPES =
    [
        "image/jpeg", "image/jpg", "image/png"
    ];

    public ImageValidator(bool required = true)
    {
        if (required)
        {
            RuleFor(i => i).NotNull().NotEmpty()
                .WithMessage("Image required");
        }

        When(i => i is not null, () =>
        {
            RuleFor(i => i.Length).NotNull().NotEmpty().LessThanOrEqualTo(IMAGE_MAX_FILE_SIZE)
                .WithMessage($"Image size is larger than allowed {IMAGE_MAX_FILE_SIZE} MB");

            RuleFor(i => i.ContentType).NotNull().NotEmpty()
                .Must(ct => ALLOWED_IMAGE_CONTENT_TYPES.Any(ct.Equals))
                .WithMessage("Allowed image types: " +
                             string.Join(", ", ALLOWED_IMAGE_CONTENT_TYPES.Select(t => $"'{t.Split('/')[1]}'")));
        });
    }
}