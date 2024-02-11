using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Entities;
using EventPlus.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Commands.Avatar;

public class SetCommandAvatarHandler(IServiceProvider serviceProvider) : MinisHandler<SetCommandAvatarRequest, string>(serviceProvider)
{
    public override async Task<string> Handle(SetCommandAvatarRequest request, CancellationToken ct)
    {
        var command = await Database.Set<Command>().SingleOrDefaultAsync(u => u.Id == request.Id, ct);

        if (command is null)
            throw new ValidationFailedException("No such Command");

        string newAvatar = string.Empty;
        
        if (request.File is not null)
        {
            newAvatar = await LoadImageToS3Bucket(request.File);
        }

        if (request.Link is not null)
        {
            newAvatar = request.Link;
        }

        command.Avatar = newAvatar;

        await Database.SaveChangesAsync(ct);

        return newAvatar;
    }

    private async Task<string> LoadImageToS3Bucket(IFormFile requestFile)
    {
        return "https://file.url";
    }
}
