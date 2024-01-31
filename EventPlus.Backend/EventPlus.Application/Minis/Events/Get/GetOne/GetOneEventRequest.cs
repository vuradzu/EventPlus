using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Events.Get.GetOne;

public class GetOneEventRequest : IMinisRequest<EventModel>
{
    public required long Id { get; set; }
}

internal sealed class GetOneEventValidator : AbstractValidator<GetOneEventRequest>
{
    public GetOneEventValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}