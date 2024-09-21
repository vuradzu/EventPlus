using EventPlus.Application.Minis.Assignments.Models;
using EventPlus.Domain.Enums;

namespace EventPlus.Application.Minis.Events.Models;

public class EventModelBase
{
    public long Id { get; init; }

    public string Title { get; init; } = default!;
    public string? Description { get; init; }

    public long CreatorId { get; set; }
    public long CommandId { get; set; }

    public Priority Priority { get; init; }
    public DateTime Date { get; init; }
    
    public string Icon { get; set; }

    public DateTime Created { get; init; }
}

public class EventModelMini : EventModelBase
{
    public int AssignmentsCount { get; set; }
    public int CompletedAssignmentsCount { get; set; }
}

public class EventModel : EventModelBase
{
    public ICollection<AssignmentModel> Assignments { get; set; }
}