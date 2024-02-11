using EventPlus.Application.Minis.Base;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Commands.Update;

public class UpdateCommandRequest : IMinisRequest
{
    [FromRoute] public long Id { get; set; } 
    public required string Name { get; init; }
    public string? Description { get; init; }
    public string? Avatar { get; init; }
}