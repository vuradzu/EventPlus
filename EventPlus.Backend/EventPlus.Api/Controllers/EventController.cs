using EventPlus.Application.Minis.Events.Create;
using EventPlus.Application.Minis.Events.Delete;
using EventPlus.Application.Minis.Events.Get.GetAll;
using EventPlus.Application.Minis.Events.Get.GetOne;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Application.Minis.Events.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Shared.Api.Constants;

namespace EventPlus.Api.Controllers;

/// <summary>
/// Events Controller
/// </summary>
[Authorize]
[ApiController]
[Route("[controller]")]
public class EventController : Controller
{
    /// <summary>
    /// Create new Event
    /// </summary>
    [Authorize(Policy = Policies.HasManageEventPermission)]
    [HttpPost("{commandId}")]
    public async Task<EventModel> Create([FromBody] CreateEventRequest request,
        [FromServices] CreateEventHandler handler, CancellationToken ct)
    {
        return await handler.Handle(request, ct);
    }
    
    /// <summary>
    /// Delete Event
    /// </summary>
    [Authorize(Policy = Policies.HasManageEventPermission)]
    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] long id,
        [FromServices] DeleteEventHandler handler, CancellationToken ct)
    {
        await handler.Handle(new DeleteEventRequest{Id = id}, ct);
    }
    
    /// <summary>
    /// Get All Events by Command
    /// </summary>
    [Authorize(Policy = Policies.HasManageEventPermission)]
    [HttpGet("by-command/{commandId}")]
    public async Task<ICollection<EventModel>> GetAll([FromRoute] long commandId,
        [FromServices] GetAllEventsHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new GetAllEventsRequest{CommandId = commandId}, ct);
    }
    
    /// <summary>
    /// Get One Event
    /// </summary>
    [Authorize(Policy = Policies.HasManageEventPermission)]
    [HttpGet("{id}")]
    public async Task<EventModel> GetOne([FromRoute] long id,
        [FromServices] GetOneEventHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new GetOneEventRequest{Id = id}, ct);
    }
    
    /// <summary>
    /// Update Event
    /// </summary>
    [Authorize(Policy = Policies.HasManageEventPermission)]
    [HttpPut("{id}")]
    public async Task Update([FromBody] UpdateEventRequest request,
        [FromServices] UpdateEventHandler handler, CancellationToken ct)
    {
        await handler.Handle(request, ct);
    }
}