using EventPlus.Application.Minis.Commands.Create;
using EventPlus.Application.Minis.Commands.Invite.Create;
using EventPlus.Application.Minis.Commands.Invite.Models;
using EventPlus.Application.Minis.Commands.Invite.Use;
using EventPlus.Application.Minis.Commands.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Shared.Api.Constants;

namespace EventPlus.Api.Controllers;

/// <summary>
/// Commands Controller
/// </summary>
[Authorize]
[ApiController]
[Route("[controller]")]
public class CommandController : Controller
{
    /// <summary>
    /// Create new Command
    /// </summary>
    [HttpPost]
    public async Task<CommandModel> Create([FromBody] CreateCommandRequest request,
        [FromServices] CreateCommandHandler handler, CancellationToken ct)
    {
        return await handler.Handle(request, ct);
    }

    /// <summary>
    /// Create invite to Command
    /// </summary>
    /// <param name="commandId">Command Id</param>
    /// <param name="maximumUses">Maximum users count, that can join to command using this code</param>
    /// <param name="validUntil">Valid until code time</param>
    [Authorize(Policy = Policies.HasManageCommandPermission)]
    [HttpPost("{commandId:long}/create-invite")]
    public async Task<InviteCodeModel> CreateInvite([FromBody] CreateInviteRequest request,
        [FromServices] CreateInviteHandler handler, CancellationToken ct)
    {
        return await handler.Handle(request, ct);
    }

    /// <summary>
    /// Use invite code, to join command
    /// </summary>
    /// <param name="code">Invite code</param>
    [HttpGet("use-invite/{code}")]
    public async Task UseInvite([FromRoute] string code, [FromServices] UseInviteHandler handler, CancellationToken ct)
    {
        await handler.Handle(new UseInviteRequest(code), ct);
    }
}