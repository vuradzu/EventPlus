using EventPlus.Application.Services;
using EventPlus.Domain.Context;
using EventPlus.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Api.Controllers;

/// <summary>
/// Testing controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class TestController(IJwtService jwtService, ISqlServerDatabase database) : Controller
{
    [HttpGet("{password}/get-jwt-token")]
    public async Task<string> GetJwtToken(string password, long userId)
    {
        if (password != "12344")
            throw new Exception("Wrong password");

        var user = await database.Set<AppUser>().FirstAsync(u => u.Id == userId);

        var jwtResult = await jwtService.GenerateAsync(user, 10003);

        return jwtResult.Token;
    }

    [AllowAnonymous]
    [HttpGet("error-endpoint")]
    public async Task ThrowError()
    {
        throw new ValidationFailedException("Помилковий екшн");
    }
}