using EventPlus.Application.Minis.Base;
using EventPlus.Application.Minis.Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.Application.Minis.Events.Get.GetAll;

public class GetAllEventsRequest : IMinisRequest<ICollection<EventModel>>
{
       public required long CommandId { get; set; }
}