using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Commands.Get.GetAll;

public class GetAllCommandsRequest : IMinisRequest<ICollection<CommandModel>>
{
    public required long Id { get; set; }
}

internal sealed class GetAllCommandsValidator : AbstractValidator<GetAllCommandsRequest>
{
    public GetAllCommandsValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid user Id");
    }
}