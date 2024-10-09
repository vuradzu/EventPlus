using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Commands.Get.SwitchCommand;

public class SwitchCommandRequest : IMinisRequest<SwitchCommandResult>
{
    public long Id { get; set; }
}

internal sealed class SwitchCommandValidator : AbstractValidator<SwitchCommandRequest>
{
    public SwitchCommandValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}