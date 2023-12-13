namespace EventPlus.Application.Minis.Commands.Invite.Models;

public class InviteCodeModel
{
    public required string Code { get; set; }
    public required long CreatedBy { get; set; }
    public required DateTimeOffset ValidUntil { get; set; }
}