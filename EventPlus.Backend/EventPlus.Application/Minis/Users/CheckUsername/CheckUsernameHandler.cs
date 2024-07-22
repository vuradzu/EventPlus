using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Context;
using EventPlus.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Application.Minis.Users.CheckUsername;

public class CheckUsernameHandler(IServiceProvider serviceProvider, ISqlServerDatabase database)
    : MinisHandler<CheckUsernameRequest, CheckUsernameResult>(serviceProvider)
{
    protected override async Task<CheckUsernameResult> Process(CheckUsernameRequest request, CancellationToken ct)
    {
        var isExist = await database.Set<AppUser>()
            .AnyAsync(u => u.NormalizedUserName == request.Username.ToUpper(), ct);

        return new(!isExist);
    }
}