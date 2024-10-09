using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Commands.Get.GetOne;

public class GetOneCommandRequest : IMinisRequest<CommandModel>
{ 
    public long Id { get; set; }
}

internal sealed class GetOneCommandValidator : AbstractValidator<GetOneCommandRequest>
{
    public GetOneCommandValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().NotNull().WithMessage("Invalid Id");
    }
}