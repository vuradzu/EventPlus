using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Assignments.Delete;

public class DeleteAssignmentRequest : IMinisRequest
{
    public required long Id { get; set; }
}

internal sealed class DeleteAssignmentValidator : AbstractValidator<DeleteAssignmentRequest>
{
    public DeleteAssignmentValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}