using EventPlus.Application.Minis.Events.Create;
using EventPlus.Application.Minis.Events.Delete;
using EventPlus.Application.Minis.Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Api.Controllers;

/// <summary>
/// Events Controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class EventController : Controller
{
    /// <summary>
    /// Create new Event
    /// </summary>
    [HttpPost("{commandId}")]
    public async Task<EventModel> Create([FromBody] CreateEventRequest request,
        [FromServices] CreateEventHandler handler, CancellationToken ct)
    {
        return await handler.Handle(request, ct);
    }

    /// <summary>
    /// Delete Event
    /// </summary>
    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] long id,
        [FromServices] DeleteEventHandler handler, CancellationToken ct)
    {
        await handler.Handle(new DeleteEventRequest{Id = id}, ct);
    }
}