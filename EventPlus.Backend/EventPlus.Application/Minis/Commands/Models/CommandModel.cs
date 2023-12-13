namespace EventPlus.Application.Minis.Commands.Models;

public class CommandModel
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public string? Avatar { get; init; }
    public long CreatorId { get; set; }

    public DateTime Created { get; init; }
}