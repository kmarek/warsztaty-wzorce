namespace Testerzy.Trainings.Romanum.Framework.Configuration.Models;

public class Settings
{
    public Application Web { get; set; } = new();
    public Api Api { get; set; } = new();
    public TestData TestData { get; set; } = new();
}
