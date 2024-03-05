using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using FluentValidation;

namespace EventPlus.Application.Minis.Assignments.Get.GetAll;

public class FilterAssignmentsRequest : IMinisRequest<ICollection<AssignmentModel>>
{
    public long? UserId { get; set; }
    public long? EventId { get; set; }
}

internal sealed class FilterAssignmentsValidator : AbstractValidator<FilterAssignmentsRequest>
{
    public FilterAssignmentsValidator()
    {
        RuleFor(a => a)
            .Must(a => a.EventId != null || a.UserId != null)
            .WithMessage("At least one filter value required");
    }
}