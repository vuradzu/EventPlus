using EventPlus.Application.Minis.Jwt.Authenticate;
using EventPlus.Application.Minis.Jwt.Refresh;
using EventPlus.Application.Services.Jwt.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Api.Controllers;

/// <summary>
/// JWT Authorization Controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class JwtController : Controller
{
    /// <summary>
    /// Authenticate user
    /// </summary>
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
    [HttpPost("refresh")]
    public async Task<JwtResult> Refresh([FromBody] JwtRefreshTokenRequest request,
        [FromServices] JwtRefreshTokenHandler handler, CancellationToken ct)
    {
        var result = await handler.Handle(request, ct);

        return result;
    }
}