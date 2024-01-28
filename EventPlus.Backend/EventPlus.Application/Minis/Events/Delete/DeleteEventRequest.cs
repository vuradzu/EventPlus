using EventPlus.Application.Minis.Base;

namespace EventPlus.Application.Minis.Events.Delete;

public class DeleteEventRequest : IMinisRequest
{ 
    public required long Id { get; set; }
}