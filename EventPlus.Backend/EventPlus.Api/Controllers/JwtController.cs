using EventPlus.Application.Minis.Jwt.Authenticate;
using EventPlus.Application.Minis.Jwt.Refresh;
using EventPlus.Application.Services.Jwt.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Api.Controllers;

/// <summary>
/// JWT Authorization Handler
/// </summary>
[ApiController]
[Route("[controller]")]
public class JwtController : Controller
{
    /// <summary>
    /// Authenticate user
    /// </summary>
    /// <param name="request"></param>
    /// <param name="handler"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("authenticate")]
    public async Task<JwtResult> Authenticate([FromBody] JwtAuthenticateRequest request,
        [FromServices] JwtAuthenticateHandler handler, CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);

        return result;
    }

    /// <summary>
    /// Refresh token
    /// </summary>
    /// <param name="request"></param>
    /// <param name="handler"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("refresh")]
    public async Task<JwtResult> Refresh([FromBody] JwtRefreshTokenRequest request,
        [FromServices] JwtRefreshTokenHandler handler, CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);

        return result;
    }
}