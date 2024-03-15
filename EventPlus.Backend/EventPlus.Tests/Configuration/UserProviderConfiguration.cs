using System.Security.Claims;
using EventPlus.Application.Services;
using EventPlus.Core.Constants;
using EventPlus.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Moq;

namespace EventPlus.Tests.Configuration;

public static class UserProviderConfiguration
{
    private static AppUser User => UserConfiguration.GetAppUserMock;

    private static readonly ICollection<Claim> UserClaims =
    [
        new(Claims.Id, User.Id.ToString()),
        new(Claims.Username, User.UserName),
        new(Claims.FirstName, User.FirstName),
        new(Claims.Avatar, User.Avatar ?? ""),
    ];

    public static Mock<IUserProvider> GetUserProviderMock()
    {
        var userProviderMock = new Mock<IUserProvider>();

        var userMock = UserConfiguration.GetAppUserMock;

        userProviderMock.SetupGet(x => x.UserId).Returns(userMock.Id);
        userProviderMock.SetupGet(x => x.UserName).Returns(userMock.UserName);


        userProviderMock.Setup(x => x.TryGetUserId())
            .Returns(userMock.Id);
        
        userProviderMock.Setup(x => x.GetUserAsync())
            .ReturnsAsync(userMock);

        userProviderMock.Setup(x => x.TryGetUserAsync())
            .ReturnsAsync(userMock);


        return userProviderMock;
    }

    private static Mock<IHttpContextAccessor> GetHttpContextAccessorMock =>
        new Mock<IHttpContextAccessor>().SetupProperty(
            x => x.HttpContext, new DefaultHttpContext { User = GetClaimsPrincipalMock().Object }
        );

    private static Mock<ClaimsPrincipal> GetClaimsPrincipalMock()
    {
        var claimsPrincipalMock = new Mock<ClaimsPrincipal>();
        claimsPrincipalMock.SetupGet(x => x.Claims).Returns(UserClaims);

        return claimsPrincipalMock;
    }
}