using EventPlus.Application.Minis.Base;
using EventPlus.Application.Services.Auth;
using EventPlus.Application.Services.Jwt.Models;
using EventPlus.Core.Extensions;
using FluentValidation;

namespace EventPlus.Application.Minis.Jwt.Authenticate;

public class JwtAuthenticateRequest:  IMinisRequest<JwtResult>
{
    public required string Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Avatar { get; set; }
    public Dictionary<string, string?> ProviderMetadata { get; set; } = default!;
    public ProviderType Provider { get; set; }
    public string ProviderKey { get; set; } = default!;
    public SsoType Type { get; set; }
}

internal sealed class SsoEnterValidator : AbstractValidator<JwtAuthenticateRequest>
{
    public SsoEnterValidator()
    {
        When(r => r.Type == SsoType.Register, () =>
        {
            RuleFor(r => r.FirstName).NotNull().NotEmpty().WithMessage("FirstName required");
            RuleFor(r => r.Username).NotNull().NotEmpty().WithMessage("Username required");
            // RuleFor(r => r.Email).NotNull().NotEmpty().WithMessage("Email required");
        });

        RuleFor(r => r.Provider).IsInEnum().WithMessage("Wrong provider");
        RuleFor(r => r.ProviderKey).NotNull().NotEmpty().WithMessage("Provider key required");
        RuleFor(r => r.ProviderMetadata).NotNull().NotEmpty().WithMessage("Provider metadata required");

        When(r => r.Provider == ProviderType.Google, () =>
        {
            RuleFor(r => r.Email).NotNull().NotEmpty().WithMessage("Email required for Google authentication");
            RuleFor(r => r.ProviderMetadata).Must(m => m.ContainsKey("token")).WithMessage("Google token required");
        });

        When(r => r.Provider == ProviderType.Telegram,
            () =>
            {
                RuleFor(r => r.ProviderMetadata).Must(m => m.ContainsKeys("auth_date", "id", "hash"))
                    .WithMessage("{auth_date, id, hash} are required");
            });
    }
}