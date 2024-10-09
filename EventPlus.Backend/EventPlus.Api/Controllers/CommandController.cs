using EventPlus.Application.Minis.Commands.Avatar;
using EventPlus.Application.Minis.Commands.Create;
using EventPlus.Application.Minis.Commands.Delete;
using EventPlus.Application.Minis.Commands.Get.GetAll;
using EventPlus.Application.Minis.Commands.Get.GetMembers;
using EventPlus.Application.Minis.Commands.Get.GetOne;
using EventPlus.Application.Minis.Commands.Get.SwitchCommand;
using EventPlus.Application.Minis.Commands.Invite.Create;
using EventPlus.Application.Minis.Commands.Invite.Models;
using EventPlus.Application.Minis.Commands.Invite.Use;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Application.Minis.Commands.Update;
using EventPlus.Application.Minis.Users.Models;
using EventPlus.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    /// <param name="commandId">Command ID</param>
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
    [HttpPost("use-invite/{code}")]
    public async Task<UseInviteResult> UseInvite([FromRoute] string code, [FromServices] UseInviteHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new UseInviteRequest(code), ct);
    }

    /// <summary>
    /// Delete Command
    /// </summary>
    [Authorize(Policy = Policies.HasManageCommandPermission)]
    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] long id,
        [FromServices] DeleteCommandHandler handler, CancellationToken ct)
    {
        await handler.Handle(new DeleteCommandRequest{Id = id}, ct);
    }
    
    /// <summary>
    /// Get All Commands by user
    /// </summary>
    [HttpGet]
    public async Task<ICollection<CommandModel>> GetAll(
        [FromServices] GetAllCommandsHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new GetAllCommandsRequest(), ct);
    }    
    
    /// <summary>
    /// Get Command by id
    /// </summary>
    [Authorize(Policy = Policies.HasCommandMemberPermission)]
    [HttpGet("{id}")]
    public async Task<CommandModel> GetOne([FromRoute] long id,
        [FromServices] GetOneCommandHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new GetOneCommandRequest{Id = id}, ct);
    }

    /// <summary>
    /// Switch command to another
    /// </summary>
    [Authorize]
    [HttpGet("switch-to/{id}")]
    public async Task<SwitchCommandResult> SwitchCommand([FromRoute] long id,
        [FromServices] SwitchCommandHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new SwitchCommandRequest{Id = id}, ct);
    }
    
    /// <summary>
    /// Update Command
    /// </summary>
    [Authorize(Policy = Policies.HasManageCommandPermission)]
    [HttpPut("{id}")]
    public async Task Update([FromBody] UpdateCommandRequest request,
        [FromServices] UpdateCommandHandler handler, CancellationToken ct)
    {
        await handler.Handle(request, ct);
    }
    
    /// <summary>
    /// Set user avatar, can be file or link
    /// </summary>
    [Authorize(Policy = Policies.HasManageCommandPermission)]
    [HttpPut("avatar")]
    public async Task<string> SetAvatar(SetCommandAvatarRequest request, [FromServices] SetCommandAvatarHandler handler,
        CancellationToken ct)
    {
        return await handler.Handle(request, ct);
    }
    
    /// <summary>
    /// Get Members by Command id
    /// </summary>
    [Authorize(Policy = Policies.HasCommandMemberPermission)]
    [HttpGet("by-command/{id}")]
    public async Task<ICollection<AppUserModelMinified>> GetMembers([FromRoute] long id,
        [FromServices] GetCommandMembersHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new GetCommandMembersRequest {Id = id}, ct);
    }   
}
