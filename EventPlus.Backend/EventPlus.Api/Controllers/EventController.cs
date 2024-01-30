using EventPlus.Application.Minis.Events.Create;
using EventPlus.Application.Minis.Events.Delete;
using EventPlus.Application.Minis.Events.Get.GetAll;
using EventPlus.Application.Minis.Events.Get.GetOne;
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
    
    /// <summary>
    /// Get All Events by Command
    /// </summary>
    [HttpGet("by-command/{commandId}")]
    public async Task<ICollection<EventModel>> GetAll([FromRoute] long commandId,
        [FromServices] GetAllEventsHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new GetAllEventsRequest{CommandId = commandId}, ct);
    }
    
    /// <summary>
    /// Get One Event
    /// </summary>
    [HttpGet("{id}")]
    public async Task<EventModel> GetOne([FromRoute] long id,
        [FromServices] GetOneEventHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new GetOneEventRequest{Id = id}, ct);
    }
    
}