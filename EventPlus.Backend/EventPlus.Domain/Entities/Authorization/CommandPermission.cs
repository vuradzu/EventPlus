using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities.Authorization;

public class CommandPermission: ICreatableEntity<long>
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;

    public DateTime Created { get; } = DateTime.UtcNow;
}