using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Assignments.Get.GetAll;

public class GetAllAssignmentRequest : IMinisRequest<ICollection<AssignmentsModel>>, IMinisRequest<ICollection<EventModel>>
{
    public long EventId { get; set; }
}

internal sealed class GetAllAssignmentsValidator : AbstractValidator<GetAllAssignmentRequest>
{
    public GetAllAssignmentsValidator()
    {
        RuleFor(i => i.EventId)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}