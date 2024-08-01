using EventPlus.Application.Minis.Events.Models;
using EventPlus.Domain.Entities;
using Mapster;

namespace EventPlus.Application.Configure;

public class MapsterConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        EventConfiguration(config);
    }

    private void EventConfiguration(TypeAdapterConfig config)
    {
        config.NewConfig<Event, EventModelMini>()
            .Map(dest => dest.AssignmentsCount, src =>
                src.Assignments == null
                    ? 0
                    : src.Assignments
                        .Count(a => a.CanBeCompleted))
            .Map(dest => dest.AssignmentsCount, src =>
                src.Assignments == null
                    ? 0
                    : src.Assignments.Count(a => a.CanBeCompleted && a.Completed));
    }
}