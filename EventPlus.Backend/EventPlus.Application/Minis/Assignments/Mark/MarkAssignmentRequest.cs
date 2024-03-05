using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Assignments.Mark;

public class MarkAssignmentRequest : IMinisRequest
{
    [FromRoute] public long Id { get; set; }
    public bool Completed  { get; set; }
}

internal sealed class MarkAssignmentValidator : AbstractValidator<MarkAssignmentRequest>
{
    public MarkAssignmentValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
        RuleFor(i => i.Completed)
            .NotEmpty().NotNull().WithMessage("Mustn't be empty");
    }
}