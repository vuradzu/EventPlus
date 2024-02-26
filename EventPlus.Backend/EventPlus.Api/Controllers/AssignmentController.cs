using EventPlus.Application.Minis.Assignments.Create;
using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Application.Minis.Commands.Create;
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
    public async Task<AssignmentsModel> Create([FromBody] CreateAssignmentRequest request,
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
    public async Task<AssignmentsModel> GetOne([FromRoute] long id,
        [FromServices] GetOneAssignmentHandler handler, CancellationToken ct)
    {
        return await handler.Handle(new GetOneAssignmentRequest(){Id = id}, ct);
    }
    
}