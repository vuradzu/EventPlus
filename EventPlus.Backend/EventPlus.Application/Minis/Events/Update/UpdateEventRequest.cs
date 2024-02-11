using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using EventPlus.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Events.Update;

public class UpdateEventRequest : IMinisRequest
{
    [FromRoute] public long Id { get; set; } 
    public string Title { get; set; }
    public string? Description { get; set; }
    
    public Priority Priority { get; set; }
    public DateTime Date { get; set; }
}