using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Application.Minis.Events.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Events.Get.GetAll;

public class GetAllEventsRequest : IMinisRequest<ICollection<EventModelMini>>, IMinisRequest<ICollection<CommandModel>>
{ 
       public required long CommandId { get; set; }
}

internal sealed class GetAllEventsValidator : AbstractValidator<GetAllEventsRequest>
{
       public GetAllEventsValidator()
       {
              RuleFor(i => i.CommandId)
                     .NotEmpty().NotNull().WithMessage("Invalid Id");
       }
}