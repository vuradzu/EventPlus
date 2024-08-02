using EventPlus.Application.Minis.Commands.Models;

namespace EventPlus.Application.Minis.Commands.Invite.Use;

public class UseInviteResult
{
    public bool IsSuccess { get; set; }
    public CommandModel? Command { get; set; }
    public CommandTokensModel? Tokens { get; set; }
    public string? Message { get; set; }
}