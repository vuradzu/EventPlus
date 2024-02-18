using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Users.Avatar;
using EventPlus.Core.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Commands.Avatar;

public class SetCommandAvatarRequest : IMinisRequest<string>
{
    public required long Id { get; set; }
    [FromForm] public IFormFile File { get; init; }
}

public class SetCommandAvatarValidator : AbstractValidator<SetUserAvatarRequest>
{
    public SetCommandAvatarValidator()
    {
        //TODO: Add validation for IFormFile
    }
}