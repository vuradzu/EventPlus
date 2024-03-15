using EventPlus.Domain.Entities.Identity;

namespace EventPlus.Tests.Configuration;

public static class UserConfiguration
{
    public static AppUser GetAppUserMock => new()
    {
        Id = 1,
        UserName = "tweeker",
        FirstName = "Vladyslav",
        LastName = "Fit"
    };
}