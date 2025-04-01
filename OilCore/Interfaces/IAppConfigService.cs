using OilCore.Models;

namespace OilCore.Interfaces;

public interface IAppConfigService
{
    /// <summary>
    /// Gets the current AppConfigModel instance. Will initialize if needed.
    /// </summary>
    AppConfigModel GetConfig();

    /// <summary>
    /// Sets and writes a new AppConfigModel instance.
    /// </summary>
    /// <param name="newConfigModel">The new AppConfigModel to store.</param>
    void SetConfig(AppConfigModel newConfigModel);

    /// <summary>
    /// Sets the AppName value in the AppConfigModel.
    /// </summary>
    /// <param name="appName">The application name to set.</param>
    void SetAppName(string appName);

    /// <summary>
    /// Event triggered when AppConfigModel is updated.
    /// </summary>
    event Action<AppConfigModel> OnConfigUpdated;
}