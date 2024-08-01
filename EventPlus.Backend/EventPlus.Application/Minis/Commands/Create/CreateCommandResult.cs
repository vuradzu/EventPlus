using EventPlus.Application.Minis.Commands.Models;

namespace EventPlus.Application.Minis.Commands.Create;

public class CreateCommandResult: CommandModel
{
    public CommandTokensModel Tokens { get; set; }
}