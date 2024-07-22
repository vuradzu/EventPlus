using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Jwt.CheckIfRegistered;

public class CheckIfProviderRegisteredRequest
    : IMinisRequest<CheckIfProviderRegisteredResult>
{
    public string Key { get; set; }
    public ProviderType Provider { get; set; }
}

internal sealed class CheckIfProviderRegisteredValidator : AbstractValidator<CheckIfProviderRegisteredRequest>
{
    public CheckIfProviderRegisteredValidator()
    {
        RuleFor(r => r.Key).NotNull().NotEmpty().WithMessage("Provider key required");
        RuleFor(r => r.Provider).IsInEnum().WithMessage("Wrong provider type");
    }
}