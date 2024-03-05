using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Users.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Commands.Get.GetMembers;

public class GetCommandMembersRequest : IMinisRequest<ICollection<AppUserModelMinified>>
{
    public required long Id { get; set; }
}

internal sealed class GetMembersCommandsValidator : AbstractValidator<GetCommandMembersRequest>
{
    public GetMembersCommandsValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}