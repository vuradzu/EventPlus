using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Commands.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Commands.Get.GetAll;

public class GetAllCommandsRequest : IMinisRequest<ICollection<CommandModel>>
{
}

internal sealed class GetAllCommandsValidator : AbstractValidator<GetAllCommandsRequest>
{
}