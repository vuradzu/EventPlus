using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Events.Create;

public class CreateEventRequest : IMinisRequest<EventModelMini>
{
    [FromRoute] public long CommandId { get; set; }

    public string Title { get; set; }
    public string? Description { get; set; }
    
    public Priority Priority { get; set; }
    public DateTime Date { get; set; }
}
 internal sealed class CreateEventValidator : AbstractValidator<CreateEventRequest>
 {
     public CreateEventValidator()
     {
         RuleFor(e => e.Title)
             .NotEmpty().NotNull().WithMessage("Wrong Title")
             .MaximumLength(30).WithMessage("Maximum Title length is 30");
         RuleFor(c => c.Description)
             .MaximumLength(100).WithMessage("Maximum description is 100");
         RuleFor(p => p.Priority)
             .NotEmpty().NotNull().IsInEnum().WithMessage("Priority is not enum type");
         RuleFor(d => d.Date >= DateTime.Now)
             .NotEmpty().NotNull().WithMessage("Wrong Time");
     }
 }