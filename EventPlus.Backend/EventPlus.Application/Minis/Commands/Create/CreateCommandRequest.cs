using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Commands.Create;

public class CreateCommandRequest: IMinisRequest<CreateCommandResult>
{
    public required string Name { get; set; } 
    public string? Description { get; set; } 
}

internal sealed class CreateCommandValidator : AbstractValidator<CreateCommandRequest>
{
    public CreateCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().NotNull().WithMessage("Wrong command name")
            .MaximumLength(30).WithMessage("Maximum command name length is 30");
        RuleFor(c => c.Description)
            .MaximumLength(100).WithMessage("Maximum command description is 100");
    }
}