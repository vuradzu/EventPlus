using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Assignments.Get.GetOne;

public class GetOneAssignmentRequest : IMinisRequest<AssignmentModel>
{
    public required long Id { get; set; }
}

internal sealed class GetOneAssignmentValidator : AbstractValidator<GetOneAssignmentRequest>
{
    public GetOneAssignmentValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}