using EventPlus.Application.Minis.Base;
using EventPlus.Domain.Context;
using EventPlus.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Application.Minis.Jwt.CheckIfRegistered;

public class CheckIfProviderRegisteredHandler(
    ISqlServerDatabase database,
    IServiceProvider serviceProvider)
    : MinisHandler<CheckIfProviderRegisteredRequest, CheckIfProviderRegisteredResult>(serviceProvider)
{
    protected override async Task<CheckIfProviderRegisteredResult> Process(CheckIfProviderRegisteredRequest request,
        CancellationToken ct)
    {
        string requestLoginProvider = request.Provider.ToString().ToLower();

        var loginInfo = await database.Set<AppUserLogin>()
            .SingleOrDefaultAsync(l =>
                l.ProviderKey == request.Key
                && l.LoginProvider == requestLoginProvider, ct);

        return new(loginInfo is not null);
    }
}