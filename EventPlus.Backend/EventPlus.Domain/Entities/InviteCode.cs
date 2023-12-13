namespace EventPlus.Domain.Entities;

public class InviteCode
{
    public required long CommandId { get; set; }
    public required string Code { get; set; }
    public long CreatorId { get; set; }
    public DateTimeOffset ValidUntil { get; set; }
    
    
    public Command? Command { get; set; }
}