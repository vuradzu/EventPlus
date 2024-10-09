using EventPlus.Application.Minis.Commands.Models;

namespace EventPlus.Application.Minis.Commands.Get.SwitchCommand;

public class SwitchCommandResult
{
    public CommandModel Command { get; set; }
    public CommandTokensModel Tokens { get; set; }
}