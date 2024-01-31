using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Events.Delete;

public class DeleteEventRequest : IMinisRequest
{ 
    public required long Id { get; set; }
}

internal sealed class DeleteEventValidator : AbstractValidator<DeleteEventRequest>
{
    public DeleteEventValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("There is no such id");
    }
}