using System.Text.Json;
using OilCore.Interfaces;
using OilCore.Models;

namespace OilCore.Services;

public class AppConfigService : IAppConfigService
{
    private readonly IOilLoggerService<AppConfigService> _logger;
    private readonly string _configFilePath;
    private readonly Lock _lock = new();
    private AppConfigModel? _appConfig;
    
    public event Action<AppConfigModel>? OnConfigUpdated;

    public AppConfigService(IOilLoggerService<AppConfigService> logger, string? configFilePath = null)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configFilePath = configFilePath ?? ConfigurationService.GetConfigurationFile();
        _logger.LogInfo($"AppConfigService initialized with config path: {_configFilePath}");
    }

    public AppConfigModel GetConfig()
    {
        EnsureConfigLoaded();
        lock (_lock)
        {
            return _appConfig!;
        }
    }

    public void SetConfig(AppConfigModel newConfigModel)
    {
        if (newConfigModel == null) throw new ArgumentNullException(nameof(newConfigModel));

        lock (_lock)
        {
            _logger.LogInfo("Updating AppConfigModel");
            _appConfig = newConfigModel;
            WriteConfig();
            OnConfigUpdated?.Invoke(_appConfig);
        }
    }

    public void SetAppName(string appName)
    {
        if (string.IsNullOrWhiteSpace(appName)) throw new ArgumentException("App name cannot be null or whitespace");

        EnsureConfigLoaded();

        lock (_lock)
        {
            _logger.LogInfo($"Setting AppName to: {appName}");
            _appConfig!.AppName = appName;
            WriteConfig();
            OnConfigUpdated?.Invoke(_appConfig);
        }
    }

    private void EnsureConfigLoaded()
    {
        if (_appConfig != null) return;

        lock (_lock)
        {
            if (_appConfig != null) return;

            _logger.LogInfo("Attempting to load AppConfigModel from disk");

            try
            {
                if (!File.Exists(_configFilePath))
                {
                    _logger.LogWarning("Config file not found. Creating default AppConfigModel.");
                    _appConfig = new AppConfigModel("DefaultApp");
                    WriteConfig();
                    return;
                }

                var json = File.ReadAllText(_configFilePath);
                _appConfig = JsonSerializer.Deserialize<AppConfigModel>(json);

                if (_appConfig == null)
                {
                    throw new InvalidOperationException("Failed to deserialize AppConfigModel");
                }

                _logger.LogInfo("AppConfigModel successfully loaded.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to load AppConfigModel. Creating fallback config.", ex);
                _appConfig = new AppConfigModel("FallbackApp");
                WriteConfig();
            }
        }
    }

    private void WriteConfig()
    {
        try
        {
            var json = JsonSerializer.Serialize(_appConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_configFilePath, json);
            _logger.LogInfo("AppConfigModel successfully written to disk.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to write AppConfigModel to disk", ex);
            throw;
        }
    }
}
