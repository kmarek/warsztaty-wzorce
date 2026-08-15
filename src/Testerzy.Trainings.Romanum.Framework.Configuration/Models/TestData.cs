namespace Testerzy.Trainings.Romanum.Framework.Configuration.Models;

public class TestData
{
    public string Prefix { get; set; } = string.Empty;
    public string DefaultPassword { get; set; } = default!;
    public List<Account> Accounts { get; set; } = [];
}
