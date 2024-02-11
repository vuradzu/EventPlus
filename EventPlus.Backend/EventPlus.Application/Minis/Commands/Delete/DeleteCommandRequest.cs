using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Commands.Delete;

public class DeleteCommandRequest : IMinisRequest
{
    public required long Id { get; set; }
}

internal sealed class DeleteCommandValidator : AbstractValidator<DeleteCommandRequest>
{
    public DeleteCommandValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}