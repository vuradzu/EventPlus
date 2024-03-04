using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Assignments.Get.GetAll.ByEvent;

public class GetAllAssignmentsByERequest : IMinisRequest<ICollection<AssignmentModel>>, IMinisRequest<ICollection<EventModel>>
{
    public long EventId { get; set; }
}

internal sealed class GetAllAssignmentsByEValidator : AbstractValidator<GetAllAssignmentsByERequest>
{
    public GetAllAssignmentsByEValidator()
    {
        RuleFor(i => i.EventId)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}