using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Commands.Get.GetOne;

public class GetOneCommandRequest : IMinisRequest<CommandModel>
{ 
    public required long Id { get; set; }
}

internal sealed class GetOneComamndValidator : AbstractValidator<GetOneCommandRequest>
{
    public GetOneComamndValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}