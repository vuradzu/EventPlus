using EventPlus.Application.Minis.Assignments.Create;
using EventPlus.Application.Minis.Assignments.Delete;
using EventPlus.Application.Minis.Assignments.Get.GetAll;
using EventPlus.Application.Minis.Assignments.Get.GetOne;
using EventPlus.Application.Minis.Assignments.Mark;
using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Assignments.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Api.Controllers;
/// <summary>
/// Assignments Controller
/// </summary>
[Authorize]
[ApiController]
[Route("[controller]")]
public class AssignmentController : Controller
{
    /// <summary>
    /// Create new Assignment
    /// </summary>
    [HttpPost]
    public async Task<AssignmentModel> Create([FromBody] CreateAssignmentRequest request,
        [FromServices] CreateAssignmentHandler handler, CancellationToken ct)
    {
        return await handler.Handle(request, ct);
    }
    
    /// <summary>
    /// Delete Assignment
    /// </summary>
    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] long id,
        [FromServices] DeleteAssignmentHandler handler, CancellationToken ct)
    {
        await handler.Handle(new DeleteAssignmentRequest(){Id = id}, ct);
    }
    
    /// <summary>
    /// Get One Assignment by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<AssignmentModel> GetOne([FromRoute] long id,
        [FromServices] GetOneAssignmentHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new GetOneAssignmentRequest(){Id = id}, ct);
    }
    
    /// <summary>
    /// Get All Assignments by Event
    /// </summary>
    [HttpGet("by-event/{eventId}")]
    public async Task<ICollection<AssignmentModel>> GetAll([FromRoute] long eventId,
        [FromServices] GetAllAssignmentsHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new GetAllAssignmentsRequest(){EventId = eventId}, ct);
    }
    
    /// <summary>
    /// Update Assignment
    /// </summary>
    [HttpPut("{id}")]
    public async Task Update([FromBody] UpdateAssignmentRequest request,
        [FromServices] UpdateAssignmentHandler handler, CancellationToken ct)
    {
        await handler.Handle(request, ct);
    }
    
    /// <summary>
    /// Mark Assignment
    /// </summary>
    [HttpPut("completed/{id}")]
    public async Task Mark([FromBody] MarkAssignmentRequest request,
        [FromServices] MarkAssignmentHandler handler, CancellationToken ct)
    {
        await handler.Handle(request, ct);
    }
}