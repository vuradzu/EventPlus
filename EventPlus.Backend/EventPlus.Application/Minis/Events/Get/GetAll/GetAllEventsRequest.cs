using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Events.Get.GetAll;

public class GetAllEventsRequest : IMinisRequest<ICollection<EventModel>>
{
       public required long CommandId { get; set; }
}

internal sealed class GetAllEventsValidator : AbstractValidator<GetAllEventsRequest>
{
       public GetAllEventsValidator()
       {
              RuleFor(i => i.CommandId)
                     .NotEmpty().NotNull().WithMessage("There is no such Command");
       }
}