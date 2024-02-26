using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Enums;
using FluentValidation;

namespace EventPlus.Application.Minis.Assignments.Create;

public class CreateAssignmentRequest: IMinisRequest<AssignmentModel>
{
    public required string Title { get; set; }
    public required string? Description { get; set; }
    
    public DateTime? Date { get; set; }
    public required Priority Priority {get; set;}

    public required bool? CanBeCompleted { get; set; }
    public required bool Completed  { get; set; }
    
    public long AssigneeId { get; set; }
    public long EventId { get; set; }
    
    public DateTime? CompletionTime  { get; }

}

internal sealed class CreateAssignmentValidator : AbstractValidator<CreateAssignmentRequest>
{
    public CreateAssignmentValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().NotNull().WithMessage("Wrong Title")
            .MaximumLength(30).WithMessage("Maximum command name length is 30");
        RuleFor(c => c.Description)
            .NotEmpty().NotNull().WithMessage("Wrong Description")
            .MaximumLength(100).WithMessage("Maximum command description is 100");
        RuleFor(c => c.Priority)
            .IsInEnum().WithMessage("Priority is not enum type");
        RuleFor(d => d.Date >= DateTime.Now)
            .NotEmpty().NotNull().WithMessage("Wrong Time");
        RuleFor(c => c.Completed)
            .NotEmpty().NotNull().WithMessage("Wrong Completed field");
        RuleFor(c => c.CanBeCompleted)
            .NotEmpty().NotNull().WithMessage("Wrong CanBeCompleted field");
        RuleFor(c => c.AssigneeId)
            .NotEmpty().NotNull().WithMessage("No assignee");
    }
}