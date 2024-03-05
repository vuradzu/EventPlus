namespace EventPlus.Application.Minis.Users.Models;

public class AppUserModel
{
    public long Id { get; set; }
    public string UserName { get; set; }

    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

    public string? Description { get; set; }
    public string? Avatar { get; set; }

    public DateTimeOffset Registered { get; set; } 
}