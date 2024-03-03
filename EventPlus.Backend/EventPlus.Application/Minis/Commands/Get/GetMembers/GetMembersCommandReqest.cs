using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using EventPlus.Domain.Entities.Identity;
using FluentValidation;

namespace EventPlus.Application.Minis.Commands.Get.GetMembers;

public class GetMembersCommandReqest : IMinisRequest<ICollection<AppUser>>
{
    public required long Id { get; set; }
}

internal sealed class GetMembersCommandsValidator : AbstractValidator<GetMembersCommandReqest>
{
    public GetMembersCommandsValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}