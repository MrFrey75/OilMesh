using OilCore.Models;

namespace OilCore.Interfaces;

public interface IAppConfigService
{
    /// <summary>
    /// Gets the current AppConfig instance. Will initialize if needed.
    /// </summary>
    AppConfig GetConfig();

    /// <summary>
    /// Sets and writes a new AppConfig instance.
    /// </summary>
    /// <param name="newConfig">The new AppConfig to store.</param>
    void SetConfig(AppConfig newConfig);

    /// <summary>
    /// Sets the AppName value in the AppConfig.
    /// </summary>
    /// <param name="appName">The application name to set.</param>
    void SetAppName(string appName);

    /// <summary>
    /// Event triggered when AppConfig is updated.
    /// </summary>
    event Action<AppConfig> OnConfigUpdated;
}