using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Application.Minis.Events.Create;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Api.Controllers;

public class EventController : Controller
{
    /// <summary>
    /// Create new Event
    /// </summary>
    [HttpPost("/{commandId}")]
    public async Task<EventModel> Create([FromRoute] long commandId, [FromBody] CreateEventRequest request,
        [FromServices] CreateEventHandler handler, CancellationToken ct)
    {
        return await handler.Handle(request with {CommandId = commandId}, ct);
    }
    
}