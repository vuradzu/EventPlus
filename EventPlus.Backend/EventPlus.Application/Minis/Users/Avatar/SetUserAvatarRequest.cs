using EventPlus.Application.Minis.Base;
using EventPlus.Core.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Users.Avatar;

public class SetUserAvatarRequest : IMinisRequest<string>
{
    [FromForm] public required IFormFile File { get; init; }
}

public class SetUserAvatarValidator : AbstractValidator<SetUserAvatarRequest>
{
    public SetUserAvatarValidator()
    {
        //TODO: Add validation for IFormFile
    }
}