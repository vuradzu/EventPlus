using EventPlus.Application.Minis.Base;
using EventPlus.Application.Services.S3;
using EventPlus.Core.Constants;
using EventPlus.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Users.Avatar;

public class SetUserAvatarHandler(IServiceProvider serviceProvider, IS3Service s3Service)
    : MinisHandler<SetUserAvatarRequest, string>(serviceProvider)
{
    protected override async Task<string> Process(SetUserAvatarRequest request, CancellationToken ct)
    {
        var user = await Database.Set<AppUser>().SingleOrDefaultAsync(u => u.Id == UserProvider.UserId, ct);

        if (user is null)
            throw new ValidationFailedException("No such user");

        if (user.Avatar is not null)
            await s3Service.DeleteFile(BucketTypes.Users, user.Avatar);

        var newAvatar = await s3Service.UploadFile(
            BucketTypes.Users,
            S3BucketsTree.UsersBucket.Avatars,
            request.File,
            user.Id.ToString());

        user.Avatar = newAvatar;

        await Database.SaveChangesAsync(ct);

        return newAvatar;
    }
}