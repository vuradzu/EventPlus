using EventPlus.Application.Minis.Commands.Create;
using EventPlus.Application.Minis.Commands.Invite;
using EventPlus.Application.Minis.Commands.Invite.Models;
using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Domain.Context;
using EventPlus.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Api.Controllers;

/// <summary>
/// Commands Controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class CommandController(ISqlServerDatabase database) : Controller
{
    [HttpGet]
    //TODO: ISoftDeletable Test
    public async Task Test()
    {
        var command = new Command
        {
            Name = "Soft",
        };

        var command2 = new Command2
        {
            Name = "Default"
        };

        await database.Set<Command>().AddAsync(command);
        await database.Set<Command2>().AddAsync(command2);

        await database.SaveChangesAsync();


        database.Set<Command>().Remove(command);
        database.Set<Command2>().Remove(command2);

        await database.SaveChangesAsync();
    }

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
    [HttpGet("{commandId:long}/create-invite")]
    public async Task<InviteCodeModel> CreateInvite(CreateInviteRequest request,
        [FromServices] CreateInviteHandler handler, CancellationToken ct)
    {
        return await handler.Handle(request, ct);
    }
}