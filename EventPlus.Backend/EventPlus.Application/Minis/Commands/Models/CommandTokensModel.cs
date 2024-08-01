namespace EventPlus.Application.Minis.Commands.Models;

public class CommandTokensModel
{
    public string Token { get; init; }

    public string RefreshToken { get; init; }

    public DateTime TokenExpires { get; init; }
    public DateTime RefreshTokenExpires { get; init; }
}