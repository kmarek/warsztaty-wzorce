namespace Testerzy.Trainings.Romanum.Framework.Configuration.Models;

public class Account
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required AccountType Type { get; set; }
}
