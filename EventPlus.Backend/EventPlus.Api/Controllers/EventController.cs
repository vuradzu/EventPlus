using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Application.Minis.Events.Create;
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
}