namespace Testerzy.Trainings.Romanum.Framework.Configuration.Models;

public class Api : Application
{
    public string? BypassKey { get; set; }
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
}
