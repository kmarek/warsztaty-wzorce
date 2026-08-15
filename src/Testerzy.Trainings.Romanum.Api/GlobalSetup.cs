using System;
using System.Collections.Generic;
using System.Text;
using Testerzy.Trainings.Romanum.Framework.Configuration;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api;

[SetUpFixture]
public class GlobalSetup
{
    public static Settings Settings { get; set; }

    [OneTimeSetUp]
    public void GlobalOneTimeSetup()
    {
        Settings = new SettingsBuilder()
            .AddAppSettings()
            .AddUserSecrets()
            .Build();
    }
}
