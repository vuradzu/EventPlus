using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;

namespace EventPlus.Application.Minis.Events.Get.GetOne;

public class GetOneEventRequest : IMinisRequest<EventModel>
{
    public required long Id { get; set; }
}