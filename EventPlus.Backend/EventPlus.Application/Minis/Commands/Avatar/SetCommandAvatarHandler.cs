using EventPlus.Application.Minis.Base;
using EventPlus.Application.Services.S3;
using EventPlus.Core.Constants;
using EventPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Commands.Avatar;

public class SetCommandAvatarHandler(IServiceProvider serviceProvider, IS3Service s3Service)
    : MinisHandler<SetCommandAvatarRequest, string>(serviceProvider)
{
    protected override async Task<string> Process(SetCommandAvatarRequest request, CancellationToken ct)
    {
        var command = await Database.Set<Command>().SingleOrDefaultAsync(u => u.Id == request.Id, ct);

        if (command is null)
            throw new ValidationFailedException("No such Command");

        if (command.Avatar is not null)
            await s3Service.DeleteFile(BucketTypes.Commands, command.Avatar);

        var newAvatar = await s3Service.UploadFile(
            BucketTypes.Commands,
            S3BucketsTree.CommandsBucket.Avatars,
            request.File,
            command.Id.ToString());

        command.Avatar = newAvatar;

        await Database.SaveChangesAsync(ct);

        return newAvatar;
    }
}