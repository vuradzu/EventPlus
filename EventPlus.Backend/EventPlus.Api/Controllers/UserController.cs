using EventPlus.Application.Minis.Users.Avatar;
using EventPlus.Application.Minis.Users.CheckUsername;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Api.Controllers;

/// <summary>
/// Users controller
/// </summary>
[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    /// <summary>
    /// Set user avatar, can be file or link
    /// </summary>
    [AllowAnonymous]
    [HttpPost("avatar")]
    public async Task<string> SetAvatar(SetUserAvatarRequest request, [FromServices] SetUserAvatarHandler handler,
        CancellationToken ct)
    {
        return await handler.Handle(request, ct);
    }

    /// <summary>
    /// Check if provider username already exists in the database
    /// </summary>
    [AllowAnonymous]
    [HttpGet("check-username/{username}")]
    public async Task<CheckUsernameResult> CheckUsername([FromRoute] string username,
        [FromServices] CheckUsernameHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new CheckUsernameRequest(username), ct);
    }
}