﻿using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Assignments.Update;

public class UpdateAssignmentRequest : IMinisRequest
{
    [FromRoute] public long Id { get; set; } 
    public string Title { get; set; }
    public string? Description { get; set; }
    
    public Priority Priority { get; set; }
    public DateTime? Date { get; set; }
    
    public bool Completed  { get; set; }
    public bool CanBeCompleted { get; set; }
    
    public long AssigneeId { get; set; } 
    public DateTime? CompletionTime  { get; set; }
}

internal sealed class UpdateAssignmentValidator : AbstractValidator<UpdateAssignmentRequest>
{
    public UpdateAssignmentValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
        RuleFor(i => i.Title)
            .NotEmpty().NotNull().WithMessage("Invalid Title");
        RuleFor(i => i.Priority)
            .NotNull().WithMessage("Invalid Priority");
        RuleFor(i => i.Completed)
            .NotEmpty().NotNull().WithMessage("'Completed' Should be true/false");
        RuleFor(i => i.CanBeCompleted)
            .NotEmpty().NotNull().WithMessage("'CanBeCompleted' Should be true/false");
        RuleFor(i => i.AssigneeId)
            .NotEmpty().NotNull().WithMessage("Invalid AssigneeId");
    }
}