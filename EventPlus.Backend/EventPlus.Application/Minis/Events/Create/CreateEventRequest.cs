using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Events.Create;

public record CreateEventRequest : IMinisRequest<EventModel>
{
    [FromRoute] public long CommandId { get; set; }

    public required string Title { get; set; }
    public string? Description { get; set; }
    
    public required Priority Priority { get; set; }
    public required DateTime Date { get; set; }
}

//== Papizje dodelau ==//
//
// internal sealed class CreateEventValidator : AbstractValidator<CreateEventRequest>
// {
//     public CreateEventValidator()
//     {
//         RuleFor(e => e.Title)
//             .NotEmpty().NotNull().WithMessage("Wrong Title")
//             .MaximumLength(30).WithMessage("Maximum Title length is 30");
//         RuleFor(c => c.Description)
//             .MaximumLength(100).WithMessage("Maximum description is 100");
//     }
// }