using EventPlus.Application.Minis.Commands.Models;
using EventPlus.Application.Minis.Events.Create;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Api.Controllers;

public class EventController : Controller
{
    /// <summary>
    /// Create new Event
    /// </summary>
    [HttpPost]
    public async Task<EventModel> Create([FromBody] CreateEventRequest request,
        [FromServices] CreateEventHandler handler, CancellationToken ct)
    {
        return await handler.Handle(request, ct);
    }
    
    /// <summary>
    /// Create new Event
    /// </summary>
}