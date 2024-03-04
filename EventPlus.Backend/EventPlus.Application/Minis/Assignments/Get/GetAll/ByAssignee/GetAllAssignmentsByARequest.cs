using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Assignments.Get.GetAll.ByAssignee;

public class GetAllAssignmentsByARequest : IMinisRequest<ICollection<AssignmentModel>>
{
    public long AssigneeId { get; set; }
}

internal sealed class GetAllAssignmentsByAValidator : AbstractValidator<GetAllAssignmentsByARequest>
{
    public GetAllAssignmentsByAValidator()
    {
        RuleFor(i => i.AssigneeId)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}