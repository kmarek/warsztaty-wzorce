using Microsoft.Extensions.Configuration;
using Testerzy.Trainings.Romanum.Framework.Configuration.Exceptions;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Framework.Configuration;

public class SettingsBuilder
{
    private readonly string _environmentName;
    private readonly Settings _settings = new();
    private readonly ConfigurationBuilder _builder = new();

    public SettingsBuilder()
    {
        _environmentName = GetEnvironmentVariable(ConfigurationConstants.EnvironmentVariableName);
    }

    public SettingsBuilder AddAppSettings()
    {        
        string defaultFileName = $"{ConfigurationConstants.AppSettingsFileName}.{ConfigurationConstants.AppSettingsFileExtension}";
        string envFileName = $"{ConfigurationConstants.AppSettingsFileName}.{_environmentName.ToLower()}.{ConfigurationConstants.AppSettingsFileExtension}";
        
        _builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(defaultFileName, optional: false, reloadOnChange: true)
                .AddJsonFile(envFileName, optional: true, reloadOnChange: true);

        return this;
    }     

    public SettingsBuilder AddUserSecrets()
    {
        _builder.AddUserSecrets<Settings>(optional: true, reloadOnChange: true);
        return this;
    }

    public SettingsBuilder AddEnvironmentVariables()
    {
        _builder.AddEnvironmentVariables();
        return this;
    }

    public Settings Build()
    {
        IConfigurationRoot? configuration = _builder.Build();
        configuration.Bind(_settings);
        configuration.GetSection(_environmentName).Bind(_settings);
        return _settings;
    }

    private static string GetEnvironmentVariable(string envVarName, bool isRequired = true)
    {
        string? envVariable = Environment.GetEnvironmentVariable(envVarName, EnvironmentVariableTarget.User)
            ?? Environment.GetEnvironmentVariable(envVarName);

        if (!string.IsNullOrEmpty(envVariable))
            return envVariable;

        if (isRequired)
            throw new SettingsException($"Environment variable '{envVarName}' is not set!");

        return string.Empty;
    }
}
